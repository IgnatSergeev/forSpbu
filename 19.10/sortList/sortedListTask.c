#include "sortedList.h"
#include <stdio.h>

bool test() {
    bool testResult = true;

    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    SortedList *sortedList = createSortedList();

    while (true) {
        int userInput = 0;
        printf("0 – выйти\n"
               "1 – добавить значение в сортированный список\n"
               "2 – удалить значение из списка\n"
               "3 – распечатать список\n");
        scanf("%d", &userInput);
        bool endCondition = false;
        switch (userInput) {
            case 0: {
                endCondition = true;
            } case 1: {
                printf("Введите значение элемента, который хотите добавить в сортированный список\n");
                int inputValue = 0;
                scanf("%d", inputValue);
                if (!insertNode(sortedList, inputValue)) {
                    printf("Возникла ошибка\n");
                }
                break;
            } case 2: {
                printf("Введите значение элемента, который хотите удалить из сортированного списка\n");
                int inputValue = 0;
                scanf("%d", inputValue);
                if (!deleteNode(sortedList, inputValue)) {
                    printf("Возникла ошибка\n");
                }
                break;
            } case 3: {
                printSortedList(sortedList);
                break;
            } default: {
                printf("Неизвестный ввод - повторите попытку\n");
            }
        }

        if (endCondition) {
            break;
        }
    }

    clearSortedList(sortedList);
    return 0;
}