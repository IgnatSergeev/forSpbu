#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <time.h>

void halfQsort(int array[], int arraySize) {
    int firstIndex = 0;
    int secondIndex = arraySize - 1;
    while ((firstIndex < secondIndex) && (firstIndex + 1 < arraySize) && (secondIndex - 1 >= 0)){
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

        return -1;
    }

    printf("%s", "Here are the random elements which will be searched\n");
    srand(time(0) * time(0));
    for (int i = 0; i < arrayOfNumbersToSearchSize - 1; i++) {
        arrayOfNumbersToSearch[i] = rand();

        printf("%d, ", arrayOfNumbersToSearch[i]);
    }
    arrayOfNumbersToSearch[arraySize - 1] = rand();
    printf("%d\n", arrayOfNumbersToSearch[arraySize - 1]);

    cleverQsort(array, 0, arraySize);
    for (int i = 0; i < arrayOfNumbersToSearchSize; i++) {
        int result = binsearch(array, arrayOfNumbersToSearch[i]);
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
