#include <stdio.h>
#include <stdbool.h>
#include <locale.h>
#include <malloc.h>
#include <time.h>

void randomlySwapTwoElements(int array[], int arraySize) {
    int firstRandomIndex = rand() % arraySize;
    int secondRandomIndex = rand() % arraySize;
    while (firstRandomIndex == secondRandomIndex) {
        secondRandomIndex = rand() % arraySize;
    }
    int temp = array[firstRandomIndex];
    array[firstRandomIndex] = array[secondRandomIndex];
    array[secondRandomIndex] = temp;
}

bool isSorted(const int array[], int arraySize) {
    for (int i = 0; i < arraySize - 1; i++) {
        if (array[i] > array[i+1]) {
            return false;
        }
    }
    return true;
}

void monkeySort(int array[], int arraySize) {
    int currentIteration = 0;
    while (!isSorted(array, arraySize)) {
        ++currentIteration;
        randomlySwapTwoElements(array, arraySize);
        printf("Текущая итерация = %d\n", currentIteration);
    }
}

bool test() {
    bool testResult = true;
    int array[] = {2, 1};
    randomlySwapTwoElements(array, 2);
    if (array[0] != 1 || array[1] != 2 || !isSorted(array, 2)) {
        testResult = false;
    }

    return testResult;
}

int main() {
    setlocale(LC_ALL, "");

    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите количество элементов массива,а затем сами элементы\n");
    int numOfArrayElements = 0;
    scanf("%d", &numOfArrayElements);
    if (numOfArrayElements <= 0) {
        printf("Размер массива должен быть положительным\n");
        return -1;
    }
    int *array = calloc(numOfArrayElements, sizeof(int));
    if (array == NULL) {
        printf("Проблемы с аллокацией памяти\n");
        return -1;
    }
    for (int i = 0; i < numOfArrayElements; i++) {
        int number = 0;
        scanf("%d", &number);
        array[i] = number;
    }

    srand((unsigned)(time(NULL)));
    monkeySort(array, numOfArrayElements);
    printf("Массив отсортирован\n");
    return 0;
}
