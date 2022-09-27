#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <math.h>
#include <time.h>

void halfQsort(int array[], int arraySize) {
    int firstIndex = 0;
    int secondIndex = 1;
    while (((firstIndex - 1) < arraySize) && (secondIndex < arraySize)) {
        if (((array[firstIndex] >= array[secondIndex]) || (array[firstIndex + 1] > array[secondIndex])) && (firstIndex!=secondIndex) ) {// && (firstIndex!=(secondIndex - 1)), нужна ли первая проверка
            firstIndex += 1;

            int temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        } else {
            secondIndex += 1;
        }
    }

    int firstElement = array[0];
    bool isSwapped = false;
    for (int i = 1; i < arraySize; i++) {
        if (array[i] >= firstElement) {
            int temp = array[i - 1];
            array[i - 1] = array[0];
            array[0] = temp;

            isSwapped = true;

            break;
        }
    }

    if (!isSwapped) {
        int temp = array[arraySize - 1];
        array[arraySize - 1] = array[0];
        array[0] = temp;
    }
}

bool tester() {
    srand((unsigned)time(0));
    int forHalfQsortArray[6] = {-1,1,-4,1,-2,1};
    halfQsort(forHalfQsortArray, 6);

    bool typicalTest = true;
    int finalArray[6] = {0};
    const int arraySize = 6;
    for (int i = 0; i < arraySize; i++) {
        printf("%d, ", forHalfQsortArray[i]);
        if (forHalfQsortArray[i] != finalArray[i]) {
            typicalTest = false;
        }
    }

    return typicalTest;
}

int main() {
    if (!tester()) {
        printf("Tests failed");

        return 0;
    } else {
        printf("Tests passed\n");
    }

    int inputBase = 0;
    int inputDegree = 0;

    printf("%s", "Enter the base and then the degree to which you want to pow\n");
    scanf("%d", &inputBase);
    scanf("%d", &inputDegree);



    return 0;
}
