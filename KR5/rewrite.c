#include <stdio.h>
#include "../Modules/queue.h"

void groupTheNumbers(Queue *queueWithNumbersLessThanLeftBorder, Queue *queueWithNumbersGreaterThanRightBorder,
                    Queue *queueWithNumbersInTheBorders, FILE *inputFile, int rightBorder, int leftBorder) {
    int currentInputNumber = 0;
    while (fscanf(inputFile, "%d", &currentInputNumber) != EOF) {
        if (currentInputNumber < leftBorder) {
            enqueue(queueWithNumbersLessThanLeftBorder, currentInputNumber);
            continue;
        }
        if (currentInputNumber > rightBorder) {
            enqueue(queueWithNumbersGreaterThanRightBorder, currentInputNumber);
            continue;
        }
        enqueue(queueWithNumbersInTheBorders, currentInputNumber);
    }
}

int printNumbersInFile(Queue *queueWithNumbersLessThanLeftBorder, Queue *queueWithNumbersGreaterThanRightBorder,
                     Queue *queueWithNumbersInTheBorders, FILE *outputFile) {
    int errorCode = 0;
    while (!isEmpty(queueWithNumbersLessThanLeftBorder)) {
        int currentNumberValue = dequeue(queueWithNumbersLessThanLeftBorder, &errorCode);
        if (errorCode) {
            return -1;
        }
        fprintf(outputFile, "%d ", currentNumberValue);
    }
    while (!isEmpty(queueWithNumbersInTheBorders)) {
        int currentNumberValue = dequeue(queueWithNumbersInTheBorders, &errorCode);
        if (errorCode) {
            return -1;
        }
        fprintf(outputFile, "%d ", currentNumberValue);
    }
    while (!isEmpty(queueWithNumbersGreaterThanRightBorder)) {
        int currentNumberValue = dequeue(queueWithNumbersGreaterThanRightBorder, &errorCode);
        if (errorCode) {
            return -1;
        }
        fprintf(outputFile, "%d ", currentNumberValue);
    }

    return 0;
}

bool test(void) {
    bool testResult = true;

    Queue *queueWithNumbersLessThanLeftBorder = createQueue();
    if (queueWithNumbersLessThanLeftBorder == NULL) {
        return false;
    }
    Queue *queueWithNumbersInTheBorders = createQueue();
    if (queueWithNumbersInTheBorders == NULL) {
        clear(queueWithNumbersLessThanLeftBorder);
        return false;
    }
    Queue *queueWithNumbersGreaterThanRightBorder = createQueue();
    if (queueWithNumbersGreaterThanRightBorder == NULL) {
        clear(queueWithNumbersLessThanLeftBorder);
        clear(queueWithNumbersInTheBorders);
        return false;
    }

    FILE *testInputFile = fopen("../KR5/rewriteTestInput.txt", "r");
    if (testInputFile == NULL) {
        clear(queueWithNumbersLessThanLeftBorder);
        clear(queueWithNumbersInTheBorders);
        clear(queueWithNumbersGreaterThanRightBorder);
        return false;
    }

    int testRightBorder = 5;
    int testLeftBorder = 4;
    groupTheNumbers(queueWithNumbersLessThanLeftBorder, queueWithNumbersGreaterThanRightBorder, queueWithNumbersInTheBorders,
                    testInputFile, testRightBorder, testLeftBorder);
    fclose(testInputFile);

    int correctNumbers[11] = {1, 3, 2, 3 ,3 ,2, 4, 5, 6, 8, 9};
    int errorCode = 0;
    for (int i = 0; i < 6; i++) {
        int currentNumber = dequeue(queueWithNumbersLessThanLeftBorder, &errorCode);
        if (errorCode) {
            clear(queueWithNumbersLessThanLeftBorder);
            clear(queueWithNumbersInTheBorders);
            clear(queueWithNumbersGreaterThanRightBorder);
            return false;
        }
        if (currentNumber != correctNumbers[i]) {
            testResult = false;
        }
    }
    for (int i = 6; i < 8; i++) {
        int currentNumber = dequeue(queueWithNumbersInTheBorders, &errorCode);
        if (errorCode) {
            clear(queueWithNumbersLessThanLeftBorder);
            clear(queueWithNumbersInTheBorders);
            clear(queueWithNumbersGreaterThanRightBorder);
            return false;
        }
        if (currentNumber != correctNumbers[i]) {
            testResult = false;
        }
    }
    for (int i = 8; i < 11; i++) {
        int currentNumber = dequeue(queueWithNumbersGreaterThanRightBorder, &errorCode);
        if (errorCode) {
            clear(queueWithNumbersLessThanLeftBorder);
            clear(queueWithNumbersInTheBorders);
            clear(queueWithNumbersGreaterThanRightBorder);
            return false;
        }
        if (currentNumber != correctNumbers[i]) {
            testResult = false;
        }
    }

    clear(queueWithNumbersLessThanLeftBorder);
    clear(queueWithNumbersInTheBorders);
    clear(queueWithNumbersGreaterThanRightBorder);
    return testResult;
}

int main(void) {
    if (test()) {
        printf("Тесты пройдены\n");
    } else {
        printf("Тесты провалены\n");
        return -1;
    }

    int leftBorder = 0;
    int rightBorder = 0;
    printf("Введите левую и правую границы отрезка, который требуется для задачи\n");
    printf("Программа возьмёт числа из файла rewriteInput.txt и разложит их на три группы, как и требуется в задаче,"
           " а затем по группам выведет их в файл rewriteOutput.txt\n");
    scanf("%d", &leftBorder);
    scanf("%d", &rightBorder);
    if (leftBorder > rightBorder) {
        printf("Левая граница не должна быть больше правой\n");
        return -1;
    }


    Queue *queueWithNumbersLessThanLeftBorder = createQueue();
    if (queueWithNumbersLessThanLeftBorder == NULL) {
        printf("Проблемы с созданием очереди\n");
        return -1;
    }
    Queue *queueWithNumbersInTheBorders = createQueue();
    if (queueWithNumbersInTheBorders == NULL) {
        printf("Проблемы с созданием очереди\n");
        clear(queueWithNumbersLessThanLeftBorder);
        return -1;
    }
    Queue *queueWithNumbersGreaterThanRightBorder = createQueue();
    if (queueWithNumbersGreaterThanRightBorder == NULL) {
        printf("Проблемы с созданием очереди\n");
        clear(queueWithNumbersLessThanLeftBorder);
        clear(queueWithNumbersInTheBorders);
        return -1;
    }

    FILE *inputFile = fopen("../KR5/rewriteInput.txt", "r");
    if (inputFile == NULL) {
        printf("Входной файл не найден\n");
        clear(queueWithNumbersLessThanLeftBorder);
        clear(queueWithNumbersInTheBorders);
        clear(queueWithNumbersGreaterThanRightBorder);
        return -1;
    }

    groupTheNumbers(queueWithNumbersLessThanLeftBorder, queueWithNumbersGreaterThanRightBorder, queueWithNumbersInTheBorders,
                    inputFile, rightBorder, leftBorder);
    fclose(inputFile);

    FILE *outputFile = fopen("../KR5/rewriteOutput.txt", "w");
    if (outputFile == NULL) {
        printf("Выходной файл не найден\n");
        clear(queueWithNumbersLessThanLeftBorder);
        clear(queueWithNumbersInTheBorders);
        clear(queueWithNumbersGreaterThanRightBorder);
        return -1;
    }

    int errorCode = printNumbersInFile(queueWithNumbersLessThanLeftBorder, queueWithNumbersGreaterThanRightBorder,
                                       queueWithNumbersInTheBorders, outputFile);
    if (errorCode) {
        printf("Ошибка при удалении элемента из очереди\n");
        clear(queueWithNumbersLessThanLeftBorder);
        clear(queueWithNumbersInTheBorders);
        clear(queueWithNumbersGreaterThanRightBorder);
        fclose(outputFile);
        return -1;
    }

    fclose(outputFile);
    clear(queueWithNumbersLessThanLeftBorder);
    clear(queueWithNumbersInTheBorders);
    clear(queueWithNumbersGreaterThanRightBorder);
    return 0;
}