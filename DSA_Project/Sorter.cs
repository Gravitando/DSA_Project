using System;
using System.Diagnostics;
using System.Globalization;


namespace VoteAnalyzingSystem
{
    
    // Abstract class for sorting algorithms
    public abstract class Sorter
    {
        public abstract void Sort(CustomLinkedList<Candidate> candidates);
        public abstract string AlgorithmName { get; }
    }

    // Bubble Sort implementation
    public class BubbleSort : Sorter
    {
        

        public override string AlgorithmName => "Bubble Sort";

        public override void Sort(CustomLinkedList<Candidate> candidates)
        {
            if (candidates.Count <= 1)
                return;

            bool swapped;
            do
            {
                swapped = false;
                var current = candidates.Head;
                while (current != null && current.Next != null)
                {
                    if (current.Value.VoteCount < current.Next.Value.VoteCount)
                    {
                        // Swap values
                        var temp = current.Value;
                        current.Value = current.Next.Value;
                        current.Next.Value = temp;
                        swapped = true;
                    }
                    current = current.Next;
                }
            } while (swapped);
        }
    }


    // Merge Sort implementation
    public class MergeSort : Sorter
    {
        public override string AlgorithmName => "Merge Sort";

        public override void Sort(CustomLinkedList<Candidate> candidates)
        {
            var sortedList = MergeSortRecursive(candidates);
            candidates.Clear();
            var current = sortedList.Head;
            while (current != null)
            {
                candidates.AddLast(current.Value);
                current = current.Next;
            }
        }

        private CustomLinkedList<Candidate> MergeSortRecursive(CustomLinkedList<Candidate> candidates)
        {
            if (candidates.Count <= 1)
                return candidates;

            var (left, right) = Split(candidates);
            left = MergeSortRecursive(left);
            right = MergeSortRecursive(right);
            return Merge(left, right);
        }

        private (CustomLinkedList<Candidate>, CustomLinkedList<Candidate>) Split(CustomLinkedList<Candidate> candidates)
        {
            var left = new CustomLinkedList<Candidate>();
            var right = new CustomLinkedList<Candidate>();
            int mid = candidates.Count / 2;

            var current = candidates.Head;
            for (int i = 0; i < mid; i++)
            {
                left.AddLast(current.Value);
                current = current.Next;
            }
            for (int i = mid; i < candidates.Count; i++)
            {
                right.AddLast(current.Value);
                current = current.Next;
            }

            return (left, right);
        }

        private CustomLinkedList<Candidate> Merge(CustomLinkedList<Candidate> left, CustomLinkedList<Candidate> right)
        {
            var result = new CustomLinkedList<Candidate>();
            var leftCurrent = left.Head;
            var rightCurrent = right.Head;

            while (leftCurrent != null && rightCurrent != null)
            {
                if (leftCurrent.Value.VoteCount >= rightCurrent.Value.VoteCount)
                {
                    result.AddLast(leftCurrent.Value);
                    leftCurrent = leftCurrent.Next;
                }
                else
                {
                    result.AddLast(rightCurrent.Value);
                    rightCurrent = rightCurrent.Next;
                }
            }

            while (leftCurrent != null)
            {
                result.AddLast(leftCurrent.Value);
                leftCurrent = leftCurrent.Next;
            }

            while (rightCurrent != null)
            {
                result.AddLast(rightCurrent.Value);
                rightCurrent = rightCurrent.Next;
            }

            return result;
        }
    }

    // Quick Sort implementation
    public class QuickSort : Sorter
    {
        public override string AlgorithmName => "Quick Sort";

        public override void Sort(CustomLinkedList<Candidate> candidates)
        {
            var list = ConvertToArray(candidates);
            QuickSortRecursive(list, 0, list.Length - 1);
            candidates.Clear();
            foreach (var candidate in list)
            {
                candidates.AddLast(candidate);
            }
        }

        private Candidate[] ConvertToArray(CustomLinkedList<Candidate> candidates)
        {
            var array = new Candidate[candidates.Count];
            var current = candidates.Head;
            for (int i = 0; i < candidates.Count; i++)
            {
                array[i] = current.Value;
                current = current.Next;
            }
            return array;
        }

        private void QuickSortRecursive(Candidate[] candidates, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(candidates, low, high);
                QuickSortRecursive(candidates, low, pi - 1);
                QuickSortRecursive(candidates, pi + 1, high);
            }
        }

        private int Partition(Candidate[] candidates, int low, int high)
        {
            int pivot = candidates[high].VoteCount;
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (candidates[j].VoteCount >= pivot)
                {
                    i++;
                    var temp = candidates[i];
                    candidates[i] = candidates[j];
                    candidates[j] = temp;
                }
            }

            var temp1 = candidates[i + 1];
            candidates[i + 1] = candidates[high];
            candidates[high] = temp1;

            return i + 1;
        }
    }
}