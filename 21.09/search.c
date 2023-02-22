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

int binSearch(const int array[], int elementToSearch, int arraySize) {
    int leftBorder = 0;
    int rightBorder = arraySize - 1;

    while (leftBorder < rightBorder - 1) {
        int middle = (leftBorder + rightBorder) / 2;
        if (array[middle] < elementToSearch) {
            leftBorder = middle;
        } else {
            rightBorder = middle;
        }
    }

    if (array[leftBorder] == elementToSearch) {
        return leftBorder;
    }
    if (array[rightBorder] == elementToSearch) {
        return rightBorder;
    }
    return -1;
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

    if (binSearch(sortedBigArray, 10, 15) != 11 || binSearch(sortedBigArray, -1, 15) != -1) {
        typicalTest = false;
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
    int arrayOfNumbersToSearchSize = 0;
    printf("%s", "Enter the arrays sizes\n");
    scanf("%d", &arraySize);
    scanf("%d", &arrayOfNumbersToSearchSize);
    if (arraySize <= 0 || arrayOfNumbersToSearchSize <= 0) {
        printf("Something wrong");

        return -1;
    }

    int *array = (int*)malloc(arraySize * sizeof(int));
    if (array == NULL) {
        printf("Error with allocation");

        return -1;
    }

    printf("%s", "Here are the random array elements\n");
    srand(time(0));
    for (int i = 0; i < arraySize - 1; i++) {
        array[i] = rand();

        printf("%d, ", array[i]);
    }
    array[arraySize - 1] = rand();
    printf("%d\n", array[arraySize - 1]);

    int *arrayOfNumbersToSearch = (int*)malloc(arrayOfNumbersToSearchSize * sizeof(int));
    if (arrayOfNumbersToSearch == NULL) {
        printf("Error with allocation");

        free(array);
        return -1;
    }

    printf("%s", "Here are the random elements which will be searched\n");
    srand(time(0) * time(0));
    for (int i = 0; i < arrayOfNumbersToSearchSize - 1; i++) {
        arrayOfNumbersToSearch[i] = rand();

        printf("%d, ", arrayOfNumbersToSearch[i]);
    }
    arrayOfNumbersToSearch[arrayOfNumbersToSearchSize - 1] = rand();
    printf("%d\n", arrayOfNumbersToSearch[arrayOfNumbersToSearchSize - 1]);

    qSort(array, 0, arraySize);
    printf("%s", "Here is the sorted array\n");
    for (int i = 0; i < arraySize - 1; i++) {
        printf("%d, ", array[i]);
    }
    printf("%d\n", array[arraySize - 1]);

    for (int i = 0; i < arrayOfNumbersToSearchSize; i++) {
        int result = binSearch(array, arrayOfNumbersToSearch[i], arraySize);
        if (result == -1) {
            printf("%d - doesnt contain\n", arrayOfNumbersToSearch[i]);
        } else {
            printf("%d - contains\n", arrayOfNumbersToSearch[i]);
        }
    }

    free(array);
    free(arrayOfNumbersToSearch);
    return 0;
}
