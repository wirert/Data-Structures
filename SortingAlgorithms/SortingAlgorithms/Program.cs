﻿// See https://aka.ms/new-console-template for more information
using SortingAlgorithms;

var arr = new int[9] { 10, 2, 8, 19, 3, 6, 4, 5, 4};

var result = arr.QuickSort();

Console.WriteLine(string.Join(" ", result));
