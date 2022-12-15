#include "../Modules/HashMap/hashMap.h"
#include <stdio.h>
#include <string.h>

bool test() {
    bool testResult = true;

    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    printf("Программа возьмёт слова из файла hashTable.txt и посчитает их частоты в нём\n");
    FILE *file = fopen("../09.11/hashTable.txt", "r");
    HashMap *hashMap = createHashMap();

    char inputString[100] = {0};
    while (fscanf(file, "%s", inputString) != EOF) {
        Type value = {0};
        strcpy(value.key, inputString);
        value.value = 1;
        addValue(hashMap, value);
    }
    fclose(file);

    float fillFactor = calculateFillFactor(hashMap);
    int maxListSize = calculateMaxListSize(hashMap);
    int middleListSize = calculateMiddleListSize(hashMap);

    printf("Вот получившиеся частоты слов:\n");
    print(hashMap);

    printf("Вот коэффициент заполнения, максимальная длина списка и средняя длина списка: %f, %d, %d\n", fillFactor, maxListSize, middleListSize);

    clearHashMap(hashMap);
    return 0;
}
