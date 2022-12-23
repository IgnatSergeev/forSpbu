#include "avlTree.h"

int compare(Type first, Type second) {
    return (first == second) ? 0 : (first > second) ? 1 : -1;
}

Type whatIfEqualInAdding(Type first, Type second) {
    return first;
}

int main() {
    AVLTree *avlTree = createAVLTree();

    addValue(avlTree, 50, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 60, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 20, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 10, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 65, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 30, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 70, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 0, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 15, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 40, &compare, &whatIfEqualInAdding);
    addValue(avlTree, 80, &compare, &whatIfEqualInAdding);
    addValue(avlTree, -10, &compare, &whatIfEqualInAdding);
    //addValue(avlTree, 25, &compare, &whatIfEqualInAdding);
    //addValue(avlTree, 35, &compare, &whatIfEqualInAdding);
    deleteValue(avlTree, 50, &compare);

}