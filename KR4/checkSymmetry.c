#include "../Modules/ListModule/customList.h"
#include <stdio.h>

bool checkSymmetry(List *list, int listSize) {
    while
}

bool test() {
    bool testResult = true;

    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    List *list = create();
    FILE *file = fopen("../KR4/checkSymmetry.txt", "r");

    int currentNum = 0;
    int listIndex = 0;
    while (fscanf(file, "%d", &currentNum)) {
        insertNode(list, currentNum, listIndex);
        ++listIndex;
    }

    int listSize = listIndex;
    bool checkResult = checkSymmetry(list, listSize);
}