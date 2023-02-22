#include "customQSort.h"
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <stdbool.h>

int *readArrayFromFile(FILE *file, int *arraySize) {
    int readBytes = fscanf(file, "%d", arraySize);
    if (readBytes < 0) {
        return NULL;
    }
    if (*arraySize <= 0) {
        return NULL;
    }

    int *array = (int*)malloc(*arraySize * sizeof(int));
    for (int i = 0; i < *arraySize; i++) {
        int element = 0;
        readBytes = fscanf(file, "%d", &element);
        if (readBytes < 0) {
            free(array);
            return NULL;
        }

        array[i] = element;
    }

    return array;
}

int mostFrequentElement(const int sortedArray[], int arraySize) {
    int mostFrequentElem = sortedArray[0];
    int maxNumberOfElements = 1;
    int currentElement = sortedArray[0];
    int currentNumberOfElements = 1;
    for (int i = 1; i < arraySize; i++) {
        if (sortedArray[i] == currentElement) {
            ++currentNumberOfElements;
        } else {
            if (currentNumberOfElements > maxNumberOfElements) {
                maxNumberOfElements = currentNumberOfElements;
                mostFrequentElem = currentElement;
            }

            currentNumberOfElements = 1;
            currentElement = sortedArray[i];
        }
    }

    return mostFrequentElem;
}

bool test() {
    bool testResult = true;

    int sortedBigArray[15] = {1, 2, 3, 4, 5, 7, 8, 9, 9, 9, 9, 10, 16, 22, 28};
    const int bigArraySize = 15;
    if (mostFrequentElement(sortedBigArray, bigArraySize) != 9) {
        testResult = false;
    }

    FILE *testFile = fopen("../28.09/modulesAndFiles/testInput.txt", "r");
    int arraySize = 0;
    int *array = readArrayFromFile(testFile, &arraySize);
    if (array == NULL) {
        return false;
    }
    testResult = testResult && (array[0] == 1 && array[1] == 2 && array[2] == 3 && array[3] == 4 && array[4] == 1);

    free(array);
    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите в файле сначала длину массива, а потом его элементы");
    FILE *file = fopen("../28.09/modulesAndFiles/input.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");

        return -1;
    }
    int arraySize = 0;
    int *array = readArrayFromFile(file, &arraySize);
    fclose(file);
    if (array == NULL) {
        printf("Возникла ошибка при чтении\n");
        return -1;
    }

    qSort(array, 0, arraySize);
    int mostFrequentElem = mostFrequentElement(array, arraySize);
    printf("Вот самый часто встречающийся элемент массива: %d\n", mostFrequentElem);

    free(array);
    return 0;
}