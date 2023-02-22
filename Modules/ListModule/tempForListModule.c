#include "customList.h"

int main() {
    List *list = create();
    insertNode(list, 4, 0);
    insertNode(list, 2, 1);
    insertNode(list, 7, 2);
    insertNode(list, 1, 3);
    insertNode(list, 5, 4);
    insertNode(list, 1, 5);
    insertNode(list, 3, 6);
    mergeSort(list);
    clear(list);
    return 0;
}
