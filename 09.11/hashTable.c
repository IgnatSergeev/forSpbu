#include "../Modules/HashMap/hashMap.h"
#include <stdio.h>
#include <string.h>

void readFileIntoHashMap(HashMap *hashMap, FILE *file) {
    char inputString[100] = {0};
    while (fscanf(file, "%s", inputString) != EOF) {
        Type value = {0};
        strcpy(value.key, inputString);
        value.value = 1;
        addValue(hashMap, value);
    }
}

bool test() {
    bool testResult = true;

    HashMap *testHashMap = createHashMap();
    if (testHashMap == NULL) {
        return false;
    }
    FILE *testFile = fopen("../09.11/testHashTable.txt", "r");
    if (testFile == NULL) {
        clearHashMap(testHashMap);
        return false;
    }
    readFileIntoHashMap(testHashMap, testFile);
    fclose(testFile);

    Type valueToSearchFor = {0};
    strcpy(valueToSearchFor.key , "as");
    Type returnValue = findValue(testHashMap, valueToSearchFor);
    if (returnValue.value == 0) {
        clearHashMap(testHashMap);
        return false;
    }
    if (returnValue.value != 3) {
        testResult = false;
    }

    strcpy(valueToSearchFor.key , "ds");
    returnValue = findValue(testHashMap, valueToSearchFor);
    if (returnValue.value == 0) {
        clearHashMap(testHashMap);
        return false;
    }
    if (returnValue.value != 1) {
        testResult = false;
    }

    strcpy(valueToSearchFor.key , "sd");
    returnValue = findValue(testHashMap, valueToSearchFor);
    if (returnValue.value == 0) {
        clearHashMap(testHashMap);
        return false;
    }
    if (returnValue.value != 2) {
        testResult = false;
    }

    clearHashMap(testHashMap);
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
    if (file == NULL) {
        printf("Файл не найден\n");
        return -1;
    }
    HashMap *hashMap = createHashMap();
    if (hashMap == NULL) {
        printf("Проблемы с создание хеш таблицы\n");
        fclose(file);
        return -1;
    }

    readFileIntoHashMap(hashMap, file);
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
