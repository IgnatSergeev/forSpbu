#include "customQSort.h"
#include <stdio.h>
#include <malloc.h>

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

int main() {
    FILE *file = fopen("input.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");

        return -1;
    }
    int arraySize = 0;
    int readBytes = fscanf(file, "%d", &arraySize);
    if (readBytes <= 0) {
        printf("Возникла ошибка при чтении\n");

        return -1;
    }

    int *array = (int*)malloc(arraySize * sizeof(int));
    for (int i = 0; i < arraySize; i++) {
        int element = 0;
        readBytes = fscanf(file, "%d", &element);
        if (readBytes <= 0) {
            printf("Возникла ошибка при чтении\n");

            free(array);
            return -1;
        }

        array[i] = element;
    }

    qSort(array, 0, arraySize);
    int mostFrequentElem = mostFrequentElement(array, arraySize);
    printf("Вот самый часто встречающийся элемент массива: %d\n", mostFrequentElem);

    free(array);
    return 0;
}