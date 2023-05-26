// Copyright 2023 Ignatii Sergeev.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.


namespace BubbleSort;

public static class Program
{
    static int Main(string[] args)
    {
        Test.TestBubbleSort();
        
        Console.WriteLine("Введите элементы массива, который хотите отсортировать, через пробел");
        var line = Console.ReadLine();
        if (line == null)
        {
            throw new Exception("Cannot sort this type of array, probably wrong input");
        }
        
        int[] array = line.Split().Select(x => int.TryParse(x, out var y) ? y: throw new Exception("Wrong array element")).ToArray();
        Sort.BubbleSort(array);
        
        Console.WriteLine("Вот отсортированный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i]);
            Console.Write(" ");
        }

        return 0;
    }
}