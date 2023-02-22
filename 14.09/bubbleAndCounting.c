#include <stdio.h>
#include <stdbool.h>
#include <malloc.h>
#include <time.h>

#define ARRAY_SIZE 10000

void bubbleSort(int inputArray[], int arraySize) {
    for (int i = 0; i < arraySize - 1; i++) {
        for (int j = 0; j < arraySize - i - 1; j++) {
            if (inputArray[j] > inputArray[j + 1]) {
                int temp = inputArray[j];
                inputArray[j] = inputArray[j + 1];
                inputArray[j + 1] = temp;
            }
        }
    }
}

void countingSort(int inputArray[], int arraySize, int *errorCode) {
    int maxElement = INT_MIN;
    int minElement = INT_MAX;
    for (int i = 0; i < arraySize; i++) {
        if (inputArray[i] < minElement) {
            minElement = inputArray[i];
        }
        if (inputArray[i] > maxElement) {
            maxElement = inputArray[i];
        }
    }

    int difference = maxElement - minElement + 1;
    int *countingArray = (int*)calloc(difference, sizeof(int));
    if (countingArray == NULL) {
        printf("Error with allocation");
        *errorCode = 1;

        return;
    }
    *errorCode = 0;
    for (int i = 0; i < arraySize; i++) {
        ++countingArray[inputArray[i] - minElement];
    }
    int arrayIndex = 0;
    for (int i = 0; i < difference; i++) {
        for (int j = 0; j < countingArray[i]; j++) {
            inputArray[arrayIndex] = i + minElement;
            ++arrayIndex;
        }
    }

    free(countingArray);
}

bool test(void) {
    int forBubbleSortArray[4] = {5, 4, 3, 2};
    int forCountingSortArray[4] = {5, 4, 3, 2};
    bubbleSort(forBubbleSortArray, 4);
    int countingFunctionErrorCode = 0;
    countingSort(forCountingSortArray, 4, &countingFunctionErrorCode);
    if (countingFunctionErrorCode == 1) {
        return false;
    }

    bool typicalTest = true;
    int sortedArray[4] = {2, 3, 4, 5};
    const int arraySize = 4;
    for (int i = 0; i < arraySize; i++) {
        if ((forBubbleSortArray[i] != sortedArray[i]) || (forCountingSortArray[i] != sortedArray[i])) {
            typicalTest = false;
        }
    }

    return typicalTest;
}

void timeTest(int *errorCode) {
    int forBubbleSortArray[ARRAY_SIZE] = {0};
    int forCountingSortArray[ARRAY_SIZE] = {0};
    for (int i = 0; i < ARRAY_SIZE; i++) {
        forBubbleSortArray[i] = ARRAY_SIZE - i;
        forCountingSortArray[i] = ARRAY_SIZE - i;
    }

    int startBubbleTime = clock();
    bubbleSort(forBubbleSortArray, ARRAY_SIZE);
    int endBubbleTime = clock();
    int bubbleTime  = endBubbleTime - startBubbleTime;

    int startCountingSortTime = clock();
    int countingFunctionErrorCode = 0;
    countingSort(forCountingSortArray, ARRAY_SIZE, &countingFunctionErrorCode);
    if (countingFunctionErrorCode == 1) {
        *errorCode = 1;

        return;
    }
    *errorCode = 0;
    int endCountingSortTime = clock();
    int countingSortTime  = endCountingSortTime - startCountingSortTime;

    printf("Bubble sort time: %d\nCounting sort time: %d", bubbleTime, countingSortTime);
}

int main(void) {

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

    int *forBubbleSortArray = (int*)malloc(arraySize * sizeof(int));
    if (forBubbleSortArray == NULL) {
        printf("Error with allocation");

        return -1;
    }

    int *forCountingSortArray = (int*)malloc(arraySize * sizeof(int));
    if (forCountingSortArray == NULL) {
        printf("Error with allocation");

        free(forBubbleSortArray);
        return -1;
    }

    printf("%s", "Now enter the array elements\n");
    for (int i = 0; i < arraySize; i++) {
        int element = 0;
        scanf("%d", &element);
        forBubbleSortArray[i] = element;
        forCountingSortArray[i] = element;
    }

    bubbleSort(forBubbleSortArray, arraySize);
    printf("Here is the result of the bubble sort:\n");
    for (int i = 0; i < (arraySize - 1); i++) {
        printf("%d, ", forBubbleSortArray[i]);
    }
    printf("%d\n", forBubbleSortArray[arraySize - 1]);

    int errorCode = 0;
    countingSort(forCountingSortArray, arraySize, &errorCode);
    if (errorCode == 1) {
        printf("Error code occurred");

        free(forBubbleSortArray);
        free(forCountingSortArray);
        return -1;
    }
    printf("Here is the result of the counting sort:\n");
    for (int i = 0; i < (arraySize - 1); i++) {
        printf("%d, ", forCountingSortArray[i]);
    }
    printf("%d\n", forCountingSortArray[arraySize - 1]);

    int errorCodeForTimeTest = 0;
    timeTest(&errorCodeForTimeTest);
    if (errorCodeForTimeTest == 1) {
        printf("Error code occurred");

        free(forBubbleSortArray);
        free(forCountingSortArray);
        return -1;
    }

    free(forBubbleSortArray);
    free(forCountingSortArray);
    return 0;
}