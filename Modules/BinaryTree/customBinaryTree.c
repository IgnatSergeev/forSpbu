#include "customBinaryTree.h"
#include <stdbool.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>

typedef struct Node {
    Type value;
    struct Node* left;
    struct Node* right;
} Node;

struct BinaryTree {
    Node *root;
};

enum Direction {
    right,
    left
};

Node *addNode(Node *node, Type value, int *errorCode, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type)) {
    *errorCode = 0;
    if (node == NULL) {
        Node *newNode = malloc(sizeof(Node));
        if (newNode == NULL) {
            *errorCode = 1;
            return node;
        }
        newNode->left = NULL;
        newNode->right = NULL;
        newNode->value = value;
        return newNode;
    }

    int compareResult = (*compare)(node->value, value);
    if (compareResult > 1 || compareResult < -1) {
        *errorCode = 1;//случай когда функция compare неправильна
        return node;
    }

    if (!compareResult) {
        node->value = (*whatIfEqualInAdding)(node->value, value);
        return node;
    }
    if (compareResult == -1) {
        node->right = addNode(node->right, value, errorCode, compare, whatIfEqualInAdding);
        return node;

    }
    node->left = addNode(node->left, value, errorCode, compare, whatIfEqualInAdding);
    return node;
}

int addValue(BinaryTree *tree, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type)) {
    int errorCode = 0;
    tree->root = addNode(tree->root, value, &errorCode, compare, whatIfEqualInAdding);
    return errorCode;
}

bool isEmpty(BinaryTree *tree) {
    return tree->root == NULL;
}

Type findNodeValue(Node *node, Type value, int *errorCode, Type zeroValue, int (*compare)(Type, Type), Type (*whatIfEqualInSearching)(Type, Type)) {
    *errorCode = 0;
    if (node == NULL) {        //случай когда нода не найдена
        return zeroValue;
    }

    int compareResult = (*compare)(node->value, value);
    if (compareResult > 1 || compareResult < -1) {
        *errorCode = 1;//случай когда функция compare неправильна
        return zeroValue;
    }

    if (!compareResult) {
        return whatIfEqualInSearching(node->value, value);
    }
    if (compareResult == -1) {
        return findNodeValue(node->right, value, errorCode, zeroValue, compare, whatIfEqualInSearching);
    }
    return findNodeValue(node->left, value, errorCode, zeroValue, compare, whatIfEqualInSearching);
}

Type findValue(BinaryTree *tree, Type value, int *errorCode, Type zeroValue, int (*compare)(Type, Type), Type (*whatIfEqualInSearching)(Type, Type)) {
    return findNodeValue(tree->root, value, errorCode, zeroValue, compare, whatIfEqualInSearching);
}


bool contains(Node *node, Type value, int *errorCode, int (*compare)(Type, Type)) {
    *errorCode = 0;
    if (node == NULL) {        //случай когда нода не найдена
        return false;
    }

    int compareResult = (*compare)(node->value, value);
    if (compareResult > 1 || compareResult < -1) {
        *errorCode = 1;//случай когда функция compare неправильна
        return false;
    }

    if (!compareResult) {
        return true;
    }
    if (compareResult == -1) {
        return contains(node->right, value, errorCode, compare);
    }
    return contains(node->left, value, errorCode, compare);
}

bool isTheValueInTree(BinaryTree *tree, Type value, int *errorCode, int (*compare)(Type, Type)) {
    return contains(tree->root, value, errorCode, compare);
}

BinaryTree *create() {
    return calloc(1, sizeof(BinaryTree));
}

void clearNode(Node *node) {
    if (node == NULL) {
        return;
    }
    clearNode(node->right);
    clearNode(node->left);
    free(node);
}

void clear(BinaryTree *tree) {
    if (isEmpty(tree)) {
        return;
    }
    clearNode(tree->root);
    free(tree);
}

Node *findAndCutOutBiggestNodeInLeftSonTree(Node *parent, enum Direction dir, Node *node) {
    if (node->right == NULL) {
        if (dir == right) {
            parent->right = node->left;
        } else {
            parent->left = node->left;
        }
        return node;
    }
    return findAndCutOutBiggestNodeInLeftSonTree(node, right, node->right);
}

Node *deleteRoot(Node *root) {
    if (root->left == NULL) {
        Node *newRoot = root->right;
        free(root);
        return newRoot;
    }
    Node *newRoot = findAndCutOutBiggestNodeInLeftSonTree(root, left, root->left);
    newRoot->right = root->right;
    newRoot->left = root->left;
    free(root);
    return newRoot;
}

int deleteNodeValue(Node *parent, enum Direction dir, Node *node, Type value, int (*compare)(Type, Type)) {
    if (node == NULL) {
        // это случай когда такой ноды нет в дереве
        return 0;
    }

    int compareResult = (*compare)(node->value, value);
    if (compareResult > 1 || compareResult < -1) {
        return -1;//случай когда функция compare неправильна
    }

    if (!compareResult) {
        Node *newNode = deleteRoot(node);
        if (dir == right) {
            parent->right = newNode;
        } else {
            parent->left = newNode;
        }
        return 0;
    }
    if (compareResult == -1) {
        return deleteNodeValue(node, right, node->right, value, compare);
    }
    return deleteNodeValue(node, left, node->left, value, compare);
}

int deleteValue(BinaryTree *tree, Type value, int (*compare)(Type, Type)) {
    if (isEmpty(tree)) {
        return 0;
    }

    int compareResult = (*compare)(tree->root->value, value);
    if (compareResult > 1 || compareResult < -1) {
        return -1;//случай когда функция compare неправильна
    }

    if (!compareResult) {
        Node *newRoot = deleteRoot(tree->root);
        tree->root = newRoot;
        return 0;
    }
    return deleteNodeValue(NULL, right, tree->root, value, compare);
}

void nodeTreeTraversal(Node *node, Type array[], int *currentArrayIndex) {
    if (node == NULL) {
        return;
    }

    nodeTreeTraversal(node->left, array, currentArrayIndex);
    array[*currentArrayIndex] = node->value;
    ++(*currentArrayIndex);
    nodeTreeTraversal(node->right, array, currentArrayIndex);
}

void treeTraversal(BinaryTree *binaryTree, Type array[]) {
    int currentArrayIndex = 0;
    nodeTreeTraversal(binaryTree->root, array, &currentArrayIndex);
}