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

int main() {



    return 0;
}
