#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <time.h>

void halfQsort(int array[], int arraySize) {
    int firstIndex = 0;
    int secondIndex = arraySize - 1;
    while ((firstIndex < secondIndex) && (firstIndex + 1 < arraySize) && (secondIndex - 1 >= 0)) {
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

bool test() {
    bool typicalTest = true;
    int array[5] = {2, 5, 1, 4, 0};
    int firstElement = 2;
    int indexOfFirstElement = 0;
    const int arraySize = 5;

    halfQsort(array, arraySize);

    for (int i = 0; i < arraySize; i++) {
        if (array[i] == firstElement) {
            indexOfFirstElement = i;
            break;
        }
    }

    for (int i = 0; i < arraySize; i++) {
        if (((i < indexOfFirstElement) && (array[i] >= firstElement))
                || ((i > indexOfFirstElement) && (array[i] < firstElement))) {
            typicalTest = false;
            break;
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
    printf("%s", "Here are the random array elements\n");

    srand(time(0));
    for (int i = 0; i < arraySize - 1; i++) {
        array[i] = rand();

        printf("%d, ", array[i]);
    }
    array[arraySize - 1] = rand();
    printf("%d\n", array[arraySize - 1]);

    printf("Here is the result of the half qsort:\n");
    halfQsort(array, arraySize);
    for (int i = 0; i < arraySize - 1; i++) {
        printf("%d, ", array[i]);
    }
    printf("%d\n", array[arraySize - 1]);

    free(array);
    return 0;
}
