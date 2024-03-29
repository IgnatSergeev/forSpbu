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

public static class Test
{
    public static void TestBubbleSort()
    {
        int[] array = { 12, 34, 5, 1 };
        Sort.BubbleSort(array);

        int[] sortedArray = { 1, 5, 12, 34 };
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != sortedArray[i])
            {
                throw new Exception("Tests failed");
            }
        }
    }
}