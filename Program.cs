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
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 256; j <= 2048; j *= 8)
                {
                    // string manipulation used to generate file names
                    string file_name = "Net_" + string.Concat(i) + "_" + string.Concat(j) + ".txt";
                    string full_path = path_input + @"\" + file_name;

                    switch (i)
                    {
                        case 1:
                            if (j == 256)
                            {
                                PopulateArray(full_path, Net_1_256);
                            }
                            else
                            {
                                PopulateArray(full_path, Net_1_2048);
                            }
                            break;
                        case 2:
                            if (j == 256)
                            {
                                PopulateArray(full_path, Net_2_256);
                            }
                            else
                            {
                                PopulateArray(full_path, Net_2_2048);
                            }
                            break;
                        case 3:
                            if (j == 256)
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

            string choice = "";
            List<int> selected_list = new List<int>();
            while (choice != "exit")
            {
                int choice_int = 0;
                while (choice_int == 0)
                {
                    Console.Clear();
                    Console.WriteLine("(1) Net_1_256");
                    Console.WriteLine("(2) Net_1_2048");
                    Console.WriteLine("(3) Net_2_256");
                    Console.WriteLine("(4) Net_2_2048");
                    Console.WriteLine("(5) Net_3_256");
                    Console.WriteLine("(6) Net_3_2048");
                    Console.WriteLine("Enter choice (1-6, or 'exit' to exit): ");

                    choice = Console.ReadLine();
                    choice_int = ValidateIntInput(choice);
                    if (choice.ToLower() == "exit" || !(choice_int >= 1 && choice_int <= 6))
                    {
                        if (!(choice_int <= 6 && choice_int >= 1))
                        {
                            Console.WriteLine("Invalid choice");
                        }
                        break;
                    }

                    int size = 0;
                    if (choice_int % 2 == 1)
                    {
                        size = 256;
                    }
                    else
                    {
                        size = 2048;
                    }

                    if (choice_int == 1) CopyArrayToList(Net_1_256, selected_list, size);
                    if (choice_int == 2) CopyArrayToList(Net_1_2048, selected_list, size);
                    if (choice_int == 3) CopyArrayToList(Net_2_256, selected_list, size);
                    if (choice_int == 4) CopyArrayToList(Net_2_2048, selected_list, size);
                    if (choice_int == 5) CopyArrayToList(Net_3_256, selected_list, size);
                    if (choice_int == 6) CopyArrayToList(Net_3_2048, selected_list, size);
                }

                int[] selected_array = selected_list.ToArray();
                choice_int = 0;
                while(choice_int == 0)
                {
                    Console.Clear();
                    if (selected_array.Length < 257) // recommend simpler sorting algorithms for smaller data sets
                    {
                        Console.WriteLine("(1) Bubble Sort (recommended)");
                        Console.WriteLine("(2) Insertion Sort (recommended)");
                        Console.WriteLine("(3) Merge Sort");
                        Console.WriteLine("(4) Quicksort");
                    }
                    else // recommend more efficient sorting algorithms for larger data sets
                    {
                        Console.WriteLine("(1) Bubble Sort");
                        Console.WriteLine("(2) Insertion Sort");
                        Console.WriteLine("(3) Merge Sort (recommended)");
                        Console.WriteLine("(4) Quicksort (recommended)");
                    }

                    choice = Console.ReadLine();
                    choice_int = ValidateIntInput(choice);
                    if(!(choice_int >= 1 && choice_int <= 4))
                    {
                        Console.WriteLine("Invalid choice");
                    }
                }

                Console.WriteLine("Sort data in ascending order or descending?");
                Console.WriteLine("Enter 'desc' for descending, anything else for ascending: ");
                choice = Console.ReadLine();

                switch(choice_int)
                {
                    case 1:
                        BubbleSort(selected_array);
                        break;
                    case 2:
                        InsertionSort(selected_array);
                        break;
                    case 3:
                        MergeSort(selected_array);
                        break;
                    case 4:
                        QuickSort(selected_array);
                        break;
                }

                Console.Clear();
                if(choice.ToLower() == "desc")
                {
                    selected_array = ReverseArray(selected_array);
                    Console.WriteLine("Array successfully sorted in descending order");
                }
                else
                {
                    Console.WriteLine("Array successfully sorted in ascending order");
                }

                choice_int = 0;
                while(choice_int == 0)
                {
                    Console.WriteLine("(1) Print sorted array");
                    Console.WriteLine("(2) Search for specific value");
                    Console.WriteLine("(3) Search for specific (or closest) value");
                    Console.WriteLine("(4) Print every 10th element");
                    if (selected_array.Length > 256) Console.WriteLine("(5) Print every 50th element");
                    Console.WriteLine("Enter choice: ");
                    choice = Console.ReadLine();

                    choice_int = ValidateIntInput(choice);
                    if(choice_int >= 1 && ((selected_array.Length > 256 && choice_int <= 5)
                        || (selected_array.Length <= 256 && choice_int <= 4)))
                    {
                        int target_int = 0;
                        switch(choice_int)
                        {
                            case 1:
                                PrintElements(selected_array, 1);
                                break;
                            case 2:
                                while (target_int == 0)
                                {
                                    Console.WriteLine("Enter target (cannot be 0): ");
                                    string target = Console.ReadLine();
                                    target_int = ValidateIntInput(target);
                                    if (target_int != 0)
                                    {
                                        SearchArray(selected_array, target_int, false);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input: target cannot be 0");
                                    }
                                }
                                break;
                            case 3:
                                while (target_int == 0)
                                {
                                    Console.WriteLine("Enter target (cannot be 0): ");
                                    string target = Console.ReadLine();
                                    target_int = ValidateIntInput(target);
                                    if (target_int != 0)
                                    {
                                        SearchArray(selected_array, target_int, true);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input: target cannot be 0");
                                    }
                                }
                                break;
                            case 4:
                                PrintElements(selected_array, 10);
                                break;
                            case 5:
                                PrintElements(selected_array, 50);
                                break;
                        }
                    }
                }
                selected_list.Clear();
                Console.WriteLine("\nDone. Press ANY KEY to continue.");
                Console.ReadLine();
            }
        }

        // Function to print all elements of an array (or every 10th or 50th, if specified)
        public static void PrintElements(int[] int_array, int interval)
        {
            for(int i = 0; i < int_array.Length; i += interval)
            {
                Console.WriteLine($"{i}. {int_array[i]}");
            }
        }

        // Function to search for all instances of an element in an array (and closest if specified)
        public static void SearchArray(int[] int_array, int target, bool closest)
        {
            int lowest_diff = 0;
            int lowest_diff_pos = 0;
            bool found = false;
            for(int pos = 0; pos < int_array.Length; pos++)
            {
                if (int_array[pos] == target)
                {
                    Console.WriteLine($"Value found: {int_array[pos]} | found at position: {pos}");
                    found = true;
                }
                int difference = Math.Abs(target - int_array[pos]);

                if (pos == 0)
                {
                    lowest_diff = difference;
                }
                else if (difference < lowest_diff)
                {
                    difference = lowest_diff;
                    lowest_diff_pos = pos;
                }
            }
            if (closest && !(found))
            {
                Console.WriteLine("Target not found");
                Console.WriteLine($"Closest value found: {lowest_diff} | found at position: {lowest_diff_pos}");
            }
        }

        // Function to reverse all elements of an array
        public static int[] ReverseArray(int[] int_array)
        {
            int array_length = int_array.Length;
            int[] array_copy = new int[array_length];
            int_array.CopyTo(array_copy, 0);
            for(int i = 0; i < array_length; i++)
            {
                array_copy[i] = int_array[array_length - 1 - i];
            }
            return array_copy;
        }

        // Function to copy all elements of an array to a list, used to create an array of arbitrary length
        public static void CopyArrayToList(int[] int_array, List<int> int_list, int size)
        {
            for(int i = 0; i < int_array.Length; i++)
            {
                int_list.Add(int_array[i]);
            }
        }

        // Function to validate an integer input for when selecting options from menus
        // Returns 0 if invalid, or the integer value itself if valid
        public static int ValidateIntInput(string input)
        {
            int value = 0;
            try
            {
                int input_int = int.Parse(input);
                value = input_int;
            }
            catch(FormatException)
            {
                // do nothing, return value as 0
            }
            return value;
        }

        public static void PopulateArray(string path, int[] a)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    a[i] = int.Parse(lines[i]);
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Files were not found at specified path. Please try again.");
                Console.WriteLine("Press ANY KEY to continue.");
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
