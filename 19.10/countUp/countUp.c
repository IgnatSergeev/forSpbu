#include "loopedList.h"
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

    LoopedList *loopedList = createLoopedList();
    printf("Введите количество войнов и какого по номеру война убивают в следующий раз\n");
    int numberOfWarriors = 0;
    int indexOfFirstDyingWarrior = 0;
    scanf("%d", &numberOfWarriors);
    scanf("%d", &indexOfFirstDyingWarrior);
    int currentWarriorToStartCountingFrom = 0;

    for (int i = 0; i < numberOfWarriors; i++) {
        insertNode(loopedList);
    }

    while (loopedListSize(loopedList) > 1) {
        int indexToDelete = (currentWarriorToStartCountingFrom + indexOfFirstDyingWarrior - 1) % loopedListSize(loopedList) + 1;
        deleteNode(loopedList, indexToDelete);
        currentWarriorToStartCountingFrom = (indexToDelete - 1) % loopedListSize(loopedList);
    }
    printf("Индекс выжившего война(при индексировании от 1) = ");
    printLoopedList(loopedList);
    clearLoopedList(loopedList);
    return 0;
}