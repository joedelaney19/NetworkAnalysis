using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetworkAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            // initalize empty data arrays
            int[] Net_1_256 = new int[256];
            int[] Net_1_2048 = new int[2048];
            int[] Net_2_256 = new int[256];
            int[] Net_2_2048 = new int[2048];
            int[] Net_3_256 = new int[256];
            int[] Net_3_2048 = new int[2048];

            // populate each array with network data from each file
            // string[] lines = System.IO.File.ReadAllLines("");

            Console.WriteLine("Enter path of files (do not include file names): ");
            string path_input = Console.ReadLine();
            string[] lines = { };
            for(int i = 1; i <= 3; i++)
            {
                for(int j = 256; j <= 2048; j *= 8)
                {
                    // string manipulation used to generate file names
                    string file_name = "Net_" + string.Concat(i) + "_" + string.Concat(j) + ".txt";
                    string full_path = path_input + @"\" + file_name;

                    switch(i)
                    {
                        case 1:
                            if(j == 256)
                            {
                                PopulateArray(full_path, Net_1_256);
                            }
                            else
                            {
                                PopulateArray(full_path, Net_1_2048);
                            }
                            break;
                        case 2:
                            if(j == 256)
                            {
                                PopulateArray(full_path, Net_2_256);
                            }
                            else
                            {
                                PopulateArray(full_path, Net_2_2048);
                            }
                            break;
                        case 3:
                            if(j == 256)
                            {
                                PopulateArray(full_path, Net_3_256);
                            }
                            else
                            {
                                PopulateArray(full_path, Net_3_2048);
                            }
                            break;
                    } // end switch
                }
            }
        }

        public static void PopulateArray(string path, int[] a)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                a[i] = int.Parse(lines[i]);
            }
        }

        static void BubbleSort(int[] a)
        {
            // get array length
            int n = a.Length;

            for (int i = 0; i < n - 1; i++) // outer loop
            {
                for (int j = 0; j < n - 1 - i; j++) // inner loop
                {
                    if (a[j] > a[j + 1]) // swap elements if first element larger than second
                    {
                        int temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                    }
                }
            }
        }

        static void InsertionSort(int[] a)
        {
            int n = a.Length;
            int i; // define i here so we can use outside scope of for loop
            int sorted = 1; // number of array elements that have been sorted

            while (sorted < n)
            {

                // shift all values larger than current to the right
                int currentTemp = a[sorted];
                for (i = sorted; i > 0; i--)
                {
                    if (currentTemp < a[i - 1])
                    {
                        a[i] = a[i - 1];
                    }
                    else
                    {
                        break;
                    }
                }
                // insert current into correct position
                a[i] = currentTemp;
                sorted++;
            }
        }

        // ----
        // Merge sort starts here
        // ----

        static void Merge(int[] a, int[] temp, int low, int mid, int high)
        {
            int ri = low;
            int ti = low;
            int di = mid;

            // while two lists are not empty, merge smaller
            while (ti < mid && di <= high)
            {
                if (a[di] < temp[ti])
                {
                    a[ri++] = a[di++];
                }
                else
                {
                    a[ri++] = temp[ti++];
                }
            }
            // possibly some values left in temp array
            while (ti < mid)
            {
                a[ri++] = temp[ti++];
            }
            // ... or some values left in data array (in correct place)
        }

        static void MergeSortRecursive(int[] a, int[] temp, int low, int high)
        {
            int n = high - low + 1;
            int mid = low + n / 2;
            int i;

            if (n < 2) return;

            // move lower half of data into temp storage
            for(i = low; i < mid; i++)
            {
                temp[i] = a[i];
            }

            // sort lower half
            MergeSortRecursive(temp, a, low, mid - 1);
            // sort upper half
            MergeSortRecursive(a, temp, mid, high);
            // merge
            Merge(a, temp, low, mid, high);
        }

        static void MergeSort(int[] a)
        {
            int n = a.Length;
            int[] temp = new int[n];
            MergeSortRecursive(a, temp, 0, n - 1);
        }

        // ----
        // Quicksort starts here
        // ----

        public static void Partition(int[] a, int left, int right)
        {
            int i, j; // initialize local left, right vars
            int pivot, temp; // initialize local pivot and temporary int for swapping

            i = left;
            j = right;
            pivot = a[(left + right) / 2]; // set pivot to middle value

            do
            {
                while ((a[i] < pivot) && (i < right)) i++;
                while ((pivot < a[j]) && (j > left)) j--;

                if (i <= j)
                {
                    temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                    i++;
                    j--;
                }
            } while (i <= j);

            if (left < j) Partition(a, left, j); // recursively sort left array
            if (i < right) Partition(a, i, right); // recursively sort right array
        }

        public static void QuickSort(int[] a)
        {
            Partition(a, 0, a.Length - 1);
        }
    }
}
