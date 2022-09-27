#include <stdio.h>
#include <stdbool.h>
#include <malloc.h>
#include <time.h>

void bubbleSort(int inputArray[], int arraySize) {
    for (int i = 0; i < (arraySize - 1); i++) {
        for (int j = 0; j < (arraySize - i - 1); j++) {
            if (inputArray[j] > inputArray[j + 1]) {
                int temp = inputArray[j];
                inputArray[j] = inputArray[j + 1];
                inputArray[j + 1] = temp;
            }
        }
    }
}

void countingSort(int inputArray[], int arraySize) {
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
    int *countingArray = (int *) calloc(difference, sizeof(int));

    for (int i = 0; i < arraySize; i++) {
        countingArray[inputArray[i] - minElement] += 1;
    }
    int arrayIndex = 0;
    for (int i = 0; i < difference; i++) {
        for (int j = 0; j < countingArray[i]; j++) {
            inputArray[arrayIndex] = i + minElement;
            arrayIndex += 1;
        }
    }

    free(countingArray);
}

bool tester() {
    int forBubbleSortArray[4] = {5, 4, 3, 2};
    int forCountingSortArray[4] = {5, 4, 3, 2};
    bubbleSort(forBubbleSortArray, 4);
    countingSort(forCountingSortArray, 4);

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

void timeTest() {
    const int arraySize = 100000;
    int forBubbleSortArray[100000] = {0};
    int forCountingSortArray[100000] = {0};
    for (int i = 0; i < arraySize; i++) {
        forBubbleSortArray[i] = arraySize - i;
        forCountingSortArray[i] = arraySize - i;
    }

    int startBubbleTime = clock();
    bubbleSort(forBubbleSortArray, arraySize);
    int endBubbleTime = clock();
    int bubbleTime  = endBubbleTime - startBubbleTime;

    int startCountingSortTime = clock();
    countingSort(forCountingSortArray, arraySize);
    int endCountingSortTime = clock();
    int countingSortTime  = endCountingSortTime - startCountingSortTime;

    printf("Bubble sort time: %d\nCounting sort time: %d",bubbleTime, countingSortTime);
}

int main() {
    if (!tester()) {
        printf("Tests failed");

        return 0;
    } else {
        printf("Tests passed\n");
    }

    int arraySize = 0;
    printf("%s", "Enter the array size\n");
    scanf("%d", &arraySize);
    if (arraySize <= 0) {
        printf("Something wrong");
        return 0;
    }

    printf("%s", "Now enter the array elements\n");
    int *forBubbleSortArray = (int *) malloc(arraySize * sizeof(int));
    int *forCountingSortArray = (int *) malloc(arraySize * sizeof(int));
    for (int i = 0; i < arraySize; i++) {
        int element = 0;
        scanf("%d", &element);
        forBubbleSortArray[i] = element;
        forCountingSortArray[i] = element;
    }

    printf("Here is the result of the bubble sort:\n");
    bubbleSort(forBubbleSortArray, arraySize);
    for (int i = 0; i < (arraySize - 1); i++) {
        printf("%d, ", forBubbleSortArray[i]);
    }
    printf("%d\n", forBubbleSortArray[arraySize - 1]);

    printf("Here is the result of the counting sort:\n");
    countingSort(forCountingSortArray, arraySize);
    for (int i = 0; i < (arraySize - 1); i++) {
        printf("%d, ", forCountingSortArray[i]);
    }
    printf("%d\n", forCountingSortArray[arraySize - 1]);

    timeTest();

    free(forBubbleSortArray);
    free(forCountingSortArray);
    return 0;
}