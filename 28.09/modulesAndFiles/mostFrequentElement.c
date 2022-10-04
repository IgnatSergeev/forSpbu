#include "customQSort.h"
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <stdbool.h>

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
    bool typicalTest = true;

    int sortedBigArray[15] = {1, 2, 3, 4, 5, 7, 8, 9, 9, 9, 9, 10, 16, 22, 28};
    const int bigArraySize = 15;
    if (mostFrequentElement(sortedBigArray, bigArraySize) != 9) {
        typicalTest = false;
    }

    return typicalTest;
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
    int readBytes = fscanf(file, "%d", &arraySize);
    if (readBytes < 0) {
        printf("Возникла ошибка при чтении\n");

        return -1;
    }
    if (arraySize <= 0) {
        printf("Размер массива должен быть положительным");

        return -1;
    }

    int *array = (int*)malloc(arraySize * sizeof(int));
    for (int i = 0; i < arraySize; i++) {
        int element = 0;
        readBytes = fscanf(file, "%d", &element);
        if (readBytes < 0) {
            printf("Возникла ошибка при чтении\n");

            free(array);
            return -1;
        }

        array[i] = element;
    }
    fclose(file);

    qSort(array, 0, arraySize);
    int mostFrequentElem = mostFrequentElement(array, arraySize);
    printf("Вот самый часто встречающийся элемент массива: %d\n", mostFrequentElem);

    free(array);
    return 0;
}