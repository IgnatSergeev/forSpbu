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


namespace BurrowsWheeler;

public static class Tests
{
    public static void BurrowsWheelerTest()
    {
        Tuple<string, int> encodedValue = BurrowsWheeler.Encode("BANANA");
        if (string.Compare(encodedValue.Item1, "NNBAAA") != 0 || encodedValue.Item2 != 3)
        {
            throw new Exception("Tests failed");
        }

        string decodedString = BurrowsWheeler.Decode(encodedValue.Item1, encodedValue.Item2);
        if (string.Compare(decodedString, "BANANA") != 0)
        {
            throw new Exception("Tests failed");
        }
    }
}