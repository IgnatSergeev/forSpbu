#include "avlTree.h"
#include <malloc.h>

typedef struct Node {
    Type value;
    int balance;
    struct Node* left;
    struct Node* right;
} Node;

struct AVLTree {
    Node *root;
};

AVLTree *createAVLTree() {
    AVLTree *avlTree = malloc(sizeof(AVLTree));
    if (avlTree == NULL) {
        return NULL;
    }

    avlTree->root = NULL;
    return avlTree;
}

bool isEmpty(AVLTree *avlTree) {
    return avlTree->root == NULL;
}

Node *rotateLeftSmall(Node *node) {
    Node *rightNode = node->right;
    node->right = rightNode->left;
    rightNode->left = node;

    if (rightNode->balance == 0) {
        rightNode->balance += 1;
        node->balance -= 1;
    } else {
        rightNode->balance -= 1;
        node->balance -= 2;
    }
    return rightNode;
}

Node *rotateRightSmall(Node *node) {
    Node *leftNode = node->left;
    node->left = leftNode->right;
    leftNode->right = node;

    if (leftNode->balance == 0) {
        node->balance += 1;
        leftNode->balance -= 1;
    } else {
        node->balance += 2;
        leftNode->balance += 1;
    }

    return leftNode;
}

Node *rotateLeftBig(Node *node) {
    node->right = rotateRightSmall(node->right);
    return rotateLeftSmall(node);
}

Node *rotateRightBig(Node *node) {
    node->left = rotateLeftSmall(node->right);
    return rotateRightSmall(node);
}

Node *balance(Node *node, bool *isBalanced) {
    if (node->balance == -2) {
        *isBalanced = true;
        if (node->left->balance <= 0) {
            return rotateRightSmall(node);
        }
        return rotateRightBig(node);
    }
    if (node->balance == 2) {
        *isBalanced = true;
        if (node->right->balance >= 0) {
            return rotateLeftSmall(node);
        }
        return rotateLeftBig(node);

    }

    return node;
}

Node *addNode(Node *node, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type),
              int *errorCode, bool *isAdded, bool *isBalanced) {
    if (node == NULL) {
        Node *newNode = malloc(sizeof(Node));
        if (newNode == NULL) {
            *errorCode = 1;
            return NULL;
        }
        newNode->value = value;
        newNode->balance = 0;
        newNode->right = NULL;
        newNode->left = NULL;
        *isAdded = true;

        return newNode;
    }

    int compareResult = (*compare)(node->value, value);
    if (compareResult > 1 || compareResult < -1) {
        *errorCode = 1;//случай когда функция compare неправильна
        return node;
    }

    if (!compareResult) {
        (*whatIfEqualInAdding)(node->value, value);
        return node;
    }

    if (compareResult == -1) {
        node->right = addNode(node->right, value, compare, whatIfEqualInAdding, errorCode, isAdded, isBalanced);
        node->right = balance(node->right, isBalanced);
        if (*isAdded && !(*isBalanced)) {
            ++node->balance;
        }

    }
    if (compareResult == 1) {
        node->left = addNode(node->left, value, compare, whatIfEqualInAdding, errorCode, isAdded, isBalanced);
        node->left = balance(node->left, isBalanced);
        if (*isAdded && !(*isBalanced)) {
            --node->balance;
        }
    }

    return node;
}

int addValue(AVLTree *avlTree, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type)) {
    if (isEmpty(avlTree)) {
        Node *newNode = malloc(sizeof(Node));
        if (newNode == NULL) {
            return -1;
        }
        newNode->value = value;
        newNode->balance = 0;
        newNode->right = NULL;
        newNode->left = NULL;

        avlTree->root = newNode;
        return 0;
    }
    int errorCode = 0;
    bool isAdded = false;
    bool isBalanced = false;
    avlTree->root = addNode(avlTree->root, value, compare, whatIfEqualInAdding, &errorCode, &isAdded, &isBalanced);
    avlTree->root = balance(avlTree->root, &isBalanced);
    return errorCode;
}