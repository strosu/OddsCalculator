using System;
using System.Collections.Generic;
using System.Linq;
using OddsCalculator.Models;

namespace OddsCalculator.Engine
{
    public class OddsCalculator
    {
        private readonly BoardState _startingState;
        private readonly Lazy<BoardStateNode> _oddsTree;
        private readonly List<BoardState> _leaves = new List<BoardState>();

        public OddsCalculator(BoardState startingState)
        {
            _startingState = startingState;
            _startingState.Probability = 1;
            _oddsTree = new Lazy<BoardStateNode>(CalculateOdds);
        }

        private BoardStateNode CalculateOdds()
        {
            var rootCopy = new BoardStateNode
            {
                Value = _startingState
            };

            CalculateRecursively(rootCopy);

            return rootCopy;
        }

        private void CalculateRecursively(BoardStateNode currentNode)
        {
            ProcessNode(currentNode);
            foreach (var child in currentNode.Children)
            {
                CalculateRecursively(child);
            }
        }

        private void ProcessNode(BoardStateNode currentNode)
        {
            if (currentNode.Value.IsLeaf)
            {
                _leaves.Add(currentNode.Value);
                return;
            }

            var possibilities = ExecuteAttack(currentNode.Value);
            var possibilityChance = currentNode.Value.Probability / possibilities.Count;

            foreach (var possibility in possibilities)
            {
                possibility.Probability = possibilityChance;
                
                currentNode.Children.Add(new BoardStateNode
                {
                    Value = possibility,
                    Parent = currentNode,
                });
            }
        }

        private List<BoardState> ExecuteAttack(BoardState currentState)
        {
            if (currentState.AttacksNext == AttacksNext.Any)
            {
                return ProcessBoardStart(currentState);
            }
            else
            {
                return ProcessNextAttack(currentState);
            }
        }

        private List<BoardState> ProcessNextAttack(BoardState currentState)
        {
            if (currentState.AttacksNext == AttacksNext.Player)
            {
                return GetPotentialTargets(currentState.EnemyMinions).Select(defendingIndex =>
                {
                    var board = currentState.Clone();
                    board.AttacksNext = AttacksNext.Enemy;
                    var attackingMinion = board.FriendlyMinions[board.NextAttackingFriendlyIndex];
                    var defendingMinion = board.EnemyMinions[defendingIndex];
                    attackingMinion.AttackEnemy(defendingMinion);

                    board.RemoveDeadMinions();

                    return board;
                }).ToList();
            }

            return GetPotentialTargets(currentState.FriendlyMinions).Select(defendingIndex =>
            {
                var board = currentState.Clone();
                board.AttacksNext = AttacksNext.Player;
                var attackingMinion = board.EnemyMinions[board.NextAttackingEnemyMinionIndex];
                var defendingMinion = board.FriendlyMinions[defendingIndex];
                attackingMinion.AttackEnemy(defendingMinion);

                board.RemoveDeadMinions();
                return board;
            }).ToList();
        }

        private List<BoardState> ProcessBoardStart(BoardState currentState)
        {
            if (currentState.FriendlyMinions.Count == currentState.EnemyMinions.Count)
            {
                return new List<BoardState>
                {
                    CloneAndDivideProbability(currentState, AttacksNext.Player),
                    CloneAndDivideProbability(currentState, AttacksNext.Enemy)
                };
            }

            var cloned = currentState.Clone();

            if (currentState.FriendlyMinions.Count < currentState.EnemyMinions.Count)
            {
                cloned.AttacksNext = AttacksNext.Enemy;
            }
            else
            {
                cloned.AttacksNext = AttacksNext.Player;
            }

            return new List<BoardState> {cloned};
        }

        private IEnumerable<int> GetPotentialTargets(List<Minion> currentStateEnemyMinions)
        {
            if (currentStateEnemyMinions.Any(x => x.HasTaunt))
            {
                for (var i = 0; i < currentStateEnemyMinions.Count; i++)
                {
                    if (currentStateEnemyMinions[i].HasTaunt)
                    {
                        yield return i;
                    }
                }
            }

            for (var i = 0; i < currentStateEnemyMinions.Count; i++)
            {
                yield return i;
            }
        }

        public double GetCurrentWinChance()
        {
            var qqq = _oddsTree.Value;
            return _leaves.Where(x => x.IsWin).Sum(x => x.Probability);
        }

        public void ProgressTo(BoardState state)
        {

        }

        private BoardState CloneAndDivideProbability(BoardState state, AttacksNext attacksNext)
        {
            var clonedState = state.Clone();
            clonedState.Probability = state.Probability / 2;
            clonedState.AttacksNext = attacksNext;
            return clonedState;
        }
    }
}