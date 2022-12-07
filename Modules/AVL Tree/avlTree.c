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

enum Direction {
    right,
    left
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

Node *rotateLeftSmall(Node *parent, enum Direction dir, Node *node) {
    Node *rightNode = node->right;
    node->right = rightNode->left;
    rightNode->left = node;
    if (dir == right) {
        parent->right = rightNode;
    } else {
        parent->left = rightNode;
    }

    return rightNode;
}

Node *rotateRightSmall(Node *parent, enum Direction dir, Node *node) {
    Node *leftNode = node->left;
    node->left = leftNode->right;
    leftNode->right = node;
    if (dir == right) {
        parent->right = leftNode;
    } else {
        parent->left = leftNode;
    }

    return leftNode;
}

Node *balance(Node *parent, enum Direction dir, Node *node) {
    if (node->balance == 2) {
        if (node->left->balance <= 0) {
            return rotateRightSmall(parent, dir, node);
        }
        return rotateRightBig();
    }
    if (node->balance == -2) {
        if (node->right->balance >= 0) {
            return rotateLeftSmall(parent, dir, node);
        }
        return rotateLeftBig();

    }
    return node;
}

Node *addNode(Node *parent, Node *node, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type), int *errorCode, bool *isAdded) {
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

    enum Direction dir = right;
    if (compareResult == -1) {
        node->right = addNode(node, node->right, value, compare, whatIfEqualInAdding, errorCode, isAdded);
        if (isAdded) {
            ++node->balance;
        }
    }
    if (compareResult == 1) {
        node->left = addNode(node, node->left, value, compare, whatIfEqualInAdding, errorCode, isAdded);
        if (isAdded) {
            --node->balance;
        }
        dir = left;
    }

    return balance(parent, dir, node);
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
    avlTree->root = addNode(avlTree->root, avlTree->root, value, compare, whatIfEqualInAdding, &errorCode, &isAdded);
    return errorCode;
}