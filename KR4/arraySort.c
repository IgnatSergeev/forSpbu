#include "../Modules/BinaryTree/customBinaryTree.h"
#include <stdio.h>

int compareFunction(int number1, int number2) {
    return (number1 < number2) ? -1 : 1;
    //Не возвращает 0, т к нам не нужно отдельно обрабатывать случай равенства чисел
}

int sortWithBinaryTree(int array[], int arraySize) {
    BinaryTree *binaryTree = create();
    if (binaryTree == NULL) {
        return -1;
    }

    for (int i = 0; i < arraySize; i++) {
        if (addValue(binaryTree, array[i], &compareFunction, NULL)) {
            clear(binaryTree);
            return -1;
        }
        //последняя функция равна null, т к при такой compareFunction мы не зайдём в условие равенства
    }
    treeTraversal(binaryTree, array);

    clear(binaryTree);
    return 0;
}

bool test(void) {
    bool testResult = true;
    int testArray[] = {1, 3, 4, 3, 2};

    int errorCode = sortWithBinaryTree(testArray, 5);
    if (errorCode) {
        return false;
    }

    int sortedTestArray[] = {1, 2, 3, 3, 4};
    for (int i = 0; i < 5; i++) {
        if (sortedTestArray[i] != testArray[i]) {
            testResult = false;
            break;
        }
    }
    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите длину, а затем элементы массива, который вы хотите отсортировать\n");
    int arraySize = 0;
    scanf("%d", &arraySize);
    if (arraySize <= 0) {
        printf("Размер массива должен быть положительным\n");
        return -1;
    }

    int *array = calloc(arraySize, sizeof(int));
    for (int i = 0; i < arraySize; i++) {
        int arrayElement = 0;
        scanf("%d", &arrayElement);
        array[i] = arrayElement;
    }

    int errorCode = sortWithBinaryTree(array, arraySize);
    if (errorCode) {
        printf("Произошла ошибка в функции сортировки\n");
        free(array);
        return -1;
    }
    printf("Вот отсортированный массив: ");
    for (int i = 0; i < arraySize; i++) {
        printf("%d ", array[i]);
    }
    printf("\n");

    free(array);
    return 0;
}