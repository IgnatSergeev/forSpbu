#include <stdio.h>
#include <stdbool.h>
#include <locale.h>
#include <malloc.h>

void insertSort(int inputArray[], int lowBorder, int highBorder) {
    for (int i = lowBorder; i < highBorder; i+=2) {
        int currentPosOfElement = i;
        while ((currentPosOfElement > lowBorder) && (inputArray[currentPosOfElement - 2] > inputArray[currentPosOfElement])) {
            int temp = inputArray[currentPosOfElement];
            inputArray[currentPosOfElement] = inputArray[currentPosOfElement - 2];
            inputArray[currentPosOfElement - 2] = temp;

            currentPosOfElement -= 2;
        }
    }
}

bool test() {
    bool typicalTest = true;
    int forInsertSortSmallArray[4] = {5, 4, 3, 2};
    insertSort(forInsertSortSmallArray, 0, 4);

    int sortedSmallArray[4] = {3, 4, 5, 2};
    const int smallArraySize = 4;
    for (int i = 0; i < smallArraySize; i++) {
        if (forInsertSortSmallArray[i] != sortedSmallArray[i]) {
            typicalTest = false;
        }
    }

    return typicalTest;
}

int main() {
    setlocale(LC_ALL, "");

    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    int arraySize = 0;
    printf("%s", "Введите длину массива\n");
    scanf("%d", &arraySize);
    if (arraySize <= 0) {
        printf("Длина массива должна быть положительна");
        
        return -1;
    }

    int *array = (int*)malloc(arraySize * sizeof(int));
    if (array == NULL) {
        printf("Проблемы с аллокацией");

        return -1;
    }

    printf("%s", "Теперь введите элементы массива\n");
    for (int i = 0; i < arraySize; i++) {
        int element = 0;
        scanf("%d", &element);
        array[i] = element;
    }

    insertSort(array, 0, arraySize);

    printf("%s", "Вот отсортированный массив\n");
    for (int i = 0; i < arraySize - 1; i++) {
        printf("%d, ", array[i]);
    }
    printf("%d\n", array[arraySize - 1]);

    free(array);
    return 0;
}
