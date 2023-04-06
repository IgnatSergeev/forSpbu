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


BurrowsWheeler.Tests.BurrowsWheelerTest();
        
Console.WriteLine("Введите строку, которую хотите трансформировать с помощью преобразования Барроуза-Уилера");
var line = Console.ReadLine();
if (string.IsNullOrEmpty(line))
{
    Console.WriteLine("Неправильный ввод");
    return;
}

var encodedValue = BurrowsWheeler.BurrowsWheeler.Encode(line);
Console.WriteLine("Вот закодированная строка и позиция конца строки в ней: " + encodedValue.Item1 + ", " + encodedValue.Item2);

var decodedValue = BurrowsWheeler.BurrowsWheeler.Decode(encodedValue.Item1, encodedValue.Item2);
Console.WriteLine("Вот строка получившаяся после декодировки: " + decodedValue);
