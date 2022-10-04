#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <time.h>

int median(int first, int second, int third) {
    if (second < first) {
        int temp = second;
        second = first;
        first = temp;
    }

    if (third < first) {
        int temp = third;
        third = first;
        first = temp;
    }

    if (third < second) {
        int temp = third;
        third = second;
        second = temp;
    }

    return second;
}

void insertSort(int inputArray[], int lowBorder, int highBorder) {
    for (int i = lowBorder; i < highBorder; i++) {
        int currentPosOfElement = i;
        while ((currentPosOfElement > lowBorder) && (inputArray[currentPosOfElement - 1] > inputArray[currentPosOfElement])) {
            int temp = inputArray[currentPosOfElement];
            inputArray[currentPosOfElement] = inputArray[currentPosOfElement - 1];
            inputArray[currentPosOfElement - 1] = temp;

            --currentPosOfElement;
        }
    }
}

void halfQsort(int array[], int lowBound, int highBound) {
    int firstIndex = lowBound;
    int secondIndex = highBound - 1;
    while ((firstIndex < secondIndex) && (firstIndex + 1 < highBound) && (secondIndex - 1 >= lowBound)){
        if (array[firstIndex] >= array[firstIndex + 1]) {
            int temp = array[firstIndex];
            array[firstIndex] = array[firstIndex + 1];
            array[firstIndex + 1] = temp;

            ++firstIndex;
        } else {
            int temp = array[firstIndex + 1];
            array[firstIndex + 1] = array[secondIndex];
            array[secondIndex] = temp;

            --secondIndex;
        }
    }
}

void qSort(int array[], int lowBorder, int highBorder) {
    if (highBorder - lowBorder < 10) {
        insertSort(array, lowBorder, highBorder);
        return;
    }

    int middleIndex = (lowBorder + highBorder)/2;
    int elementToSplitBy = median(array[lowBorder], array[highBorder - 1], array[middleIndex]);
    int indOfElementToSplitBy = 0;
    if (elementToSplitBy == array[lowBorder]) {
        indOfElementToSplitBy = lowBorder;
    }
    if (elementToSplitBy == array[highBorder - 1]) {
        indOfElementToSplitBy = highBorder - 1;
    }
    if (elementToSplitBy == array[middleIndex]) {
        indOfElementToSplitBy = middleIndex;
    }

    int temp = array[indOfElementToSplitBy];
    array[indOfElementToSplitBy] = array[lowBorder];
    array[lowBorder] = temp;

    halfQsort(array, lowBorder, highBorder);
    for (int i = lowBorder; i < highBorder; i++) {
        if (array[i] == elementToSplitBy) {
            indOfElementToSplitBy = i;
            break;
        }
    }

    qSort(array, lowBorder, indOfElementToSplitBy);
    qSort(array, indOfElementToSplitBy + 1, highBorder);
}

int mostFrequentElement(const int sortedArray[], int arraySize) {
    int mostFrequentElem = 0;
    int maxNumberOfElements = 0;
    bool isCurrentElementDefined = false;
    int currentElement = 0;
    int currentNumberOfElements = 0;
    for (int i = 0; i < arraySize; i++) {
        if (!isCurrentElementDefined) {
            currentElement = sortedArray[i];
            currentNumberOfElements = 1;
            isCurrentElementDefined = true;
        }

        if (sortedArray[i] == currentElement) {
            ++currentNumberOfElements;
        } else {
            if (currentNumberOfElements > maxNumberOfElements) {
                maxNumberOfElements = currentNumberOfElements;
                mostFrequentElem = currentElement;
            }
        }
     }

    return mostFrequentElem;
}

bool test() {
    bool typicalTest = true;
    int forQSortBigArray[15] = {5, 4, 3, 2, 8 , 28, 16, 1, 10, 9, 9, 9, 9, 7, 22};
    qSort(forQSortBigArray, 0, 15);

    int sortedBigArray[15] = {1, 2, 3, 4, 5, 7, 8, 9, 9, 9, 9, 10, 16, 22, 28};
    const int bigArraySize = 15;
    for (int i = 0; i < bigArraySize; i++) {
        if (forQSortBigArray[i] != sortedBigArray[i]) {
            typicalTest = false;
        }
    }

    return typicalTest;
}

int main() {
    if (!test()) {
        printf("Tests failed");

        return -1;
    } else {
        printf("Tests passed\n");
    }

    int arraySize = 0;
    printf("%s", "Enter the array size\n");
    scanf("%d", &arraySize);
    if (arraySize <= 0) {
        printf("Something wrong");

        return -1;
    }

    int *array = (int*)malloc(arraySize * sizeof(int));
    if (array == NULL) {
        printf("Error with allocation");

        return -1;
    }

    printf("%s", "Enter the array elements\n");
    for (int i = 0; i < arraySize; i++) {
        int element = 0;
        scanf("%d", &element);
        array[i] = element;
    }

    qSort(array, 0, arraySize);

    free(array);
    return 0;
}
