#include "customList.h"

int main() {
    List *list = create();
    insertNode(list, 2, 0);
    clear(list);
    return 0;
}
