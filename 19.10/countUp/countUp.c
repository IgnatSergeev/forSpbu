#include "loopedList.h"
#include <stdio.h>

void calculateSurvivorIndex(LoopedList *loopedList, int numberOfWarriors, int indexOfFirstDyingWarrior) {
    int currentWarriorToStartCountingFrom = 0;

    for (int i = 0; i < numberOfWarriors; i++) {
        insertNode(loopedList);
    }

    while (loopedListSize(loopedList) > 1) {
        int indexToDelete = (currentWarriorToStartCountingFrom + indexOfFirstDyingWarrior - 1) % loopedListSize(loopedList) + 1;
        deleteNode(loopedList, indexToDelete);
        currentWarriorToStartCountingFrom = (indexToDelete - 1) % loopedListSize(loopedList);
    }
}

bool test() {
    bool testResult = true;
    LoopedList *testLoopedList = createLoopedList();
    int testNumberOfWarriors = 10;
    int testIndexOfFirstDyingWarrior = 2;
    int errorCode = 0;

    calculateSurvivorIndex(testLoopedList, testNumberOfWarriors, testIndexOfFirstDyingWarrior);
    if (loopedListSize(testLoopedList) != 1 || top(testLoopedList, &errorCode) != 5) {
        if (errorCode) {
            testResult = false;
        }
        testResult = false;
    }
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

    calculateSurvivorIndex(loopedList, numberOfWarriors, indexOfFirstDyingWarrior);

    printf("Индекс выжившего война(при индексировании от 1) = ");
    printLoopedList(loopedList);
    clearLoopedList(loopedList);
    return 0;
}