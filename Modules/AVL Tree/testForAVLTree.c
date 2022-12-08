#include "avlTree.h"

int compare(Type first, Type second) {
    return (first == second) ? 0 : (first > second) ? 1 : -1;
}

Type whatIfEqualInAdding(Type first, Type second) {
    return first;
}

int main() {
    AVLTree *avlTree = createAVLTree();

    addValue(avlTree, 2, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 3, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 4, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 5, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 6, &compare, &whatIfEqualInAdding);

}