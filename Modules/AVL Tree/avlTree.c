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

Node *rotateLeftSmall(Node *node, bool *isBalancingStopped, bool isAdding) {
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

    if (isBalancingStopped != NULL && isAdding && rightNode->balance == 0) {
        *isBalancingStopped = true;
    }
    if (isBalancingStopped != NULL && !isAdding && (rightNode->balance == -1 || rightNode->balance == 1)) {
        *isBalancingStopped = true;
    }

    return rightNode;
}

Node *rotateRightSmall(Node *node, bool *isBalancingStopped, bool isAdding) {
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

    if (isBalancingStopped != NULL && isAdding && leftNode->balance == 0) {
        *isBalancingStopped = true;
    }
    if (isBalancingStopped != NULL && !isAdding && (leftNode->balance == -1 || leftNode->balance == 1)) {
        *isBalancingStopped = true;
    }

    return leftNode;
}

Node *rotateLeftBig(Node *node, bool *isBalancingStopped, bool isAdding) {
    node->right = rotateRightSmall(node->right, NULL, isAdding);
    return rotateLeftSmall(node, isBalancingStopped, isAdding);
}

Node *rotateRightBig(Node *node, bool *isBalancingStopped, bool isAdding) {
    node->left = rotateLeftSmall(node->right, NULL, isAdding);
    return rotateRightSmall(node, isBalancingStopped, isAdding);
}

Node *balance(Node *node, bool *isBalancingStopped, bool isAdding) {
    if (node->balance == -2) {
        if (node->left->balance <= 0) {
            return rotateRightSmall(node, isBalancingStopped, isAdding);
        }
        return rotateRightBig(node, isBalancingStopped, isAdding);
    }
    if (node->balance == 2) {
        if (node->right->balance >= 0) {
            return rotateLeftSmall(node, isBalancingStopped, isAdding);
        }
        return rotateLeftBig(node, isBalancingStopped, isAdding);

    }

    return node;
}

Node *addNode(Node *node, Type value, int (*compare)(Type, Type), void (*whatIfEqualInAdding)(Type, Type),
              int *errorCode, bool *isAdded, bool *isBalancingStopped) {
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
        node->right = addNode(node->right, value, compare, whatIfEqualInAdding, errorCode, isAdded, isBalancingStopped);

        if (*isAdded && !(*isBalancingStopped)) {
            ++node->balance;
        }
    }
    if (compareResult == 1) {
        node->left = addNode(node->left, value, compare, whatIfEqualInAdding, errorCode, isAdded, isBalancingStopped);

        if (*isAdded && !(*isBalancingStopped)) {
            --node->balance;
        }
    }

    if (node->balance == 0) {
        *isBalancingStopped = true;
    }

    return balance(node, isBalancingStopped, true);
}

int addValue(AVLTree *avlTree, Type value, int (*compare)(Type, Type), void (*whatIfEqualInAdding)(Type, Type)) {
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
    bool isBalancingStopped = false;
    avlTree->root = addNode(avlTree->root, value, compare, whatIfEqualInAdding, &errorCode, &isAdded, &isBalancingStopped);
    return errorCode;
}

Node *findAndCutOutBiggestNodeInLeftSonTree(Node *node, bool *isBalancingStopped, Node **nodeForSearch) {
    if (node->right == NULL) {

        *nodeForSearch = node;
        return node->left;
    }
    node->right = findAndCutOutBiggestNodeInLeftSonTree(node->right, isBalancingStopped, nodeForSearch);
    if (!(*isBalancingStopped)) {
        node->balance -= 1;
    }

    if (node->balance == -1 || node->balance == 1) {
        *isBalancingStopped = true;
    }

    return balance(node, isBalancingStopped, false);
}

Node *deleteRoot(Node *root, bool *isBalancingStopped) {
    if (root->left == NULL) {
        Node *newRoot = root->right;
        free(root);
        return newRoot;
    }
    Node **nodeForSearch = malloc(sizeof(Node *));
    root->left = findAndCutOutBiggestNodeInLeftSonTree(root->left, isBalancingStopped, nodeForSearch);
    Node *newRoot = *nodeForSearch;
    if (!(*isBalancingStopped)) {
        root->balance += 1;
    }

    if (root->balance == -1 || root->balance == 1) {
        *isBalancingStopped = true;
    }

    newRoot->balance = root->balance;
    newRoot->right = root->right;
    newRoot->left = root->left;
    free(root);
    newRoot = balance(newRoot, isBalancingStopped, false);
    return newRoot;
}

Node *deleteNodeValue(Node *node, Type value, int (*compare)(Type, Type), bool *isDeleted, int *errorCode, bool *isBalancingStopped, Type *valueOfDeletedElement) {
    *errorCode = 0;
    if (node == NULL) {
        return node;//это случай когда такой ноды нет в дереве
    }

    int compareResult = (*compare)(node->value, value);
    if (compareResult > 1 || compareResult < -1) {
        *errorCode = 1;
        return node;//случай когда функция compare неправильна
    }

    if (!compareResult) {
        *isDeleted = true;
        *valueOfDeletedElement = node->value;
        Node *newNode = deleteRoot(node, isBalancingStopped);
        return newNode;
    }
    if (compareResult == -1) {
        node->right = deleteNodeValue(node->right, value, compare, isDeleted, errorCode, isBalancingStopped, valueOfDeletedElement);
        if (*isDeleted && !(*isBalancingStopped)) {
            node->balance -= 1;
        }
    } else {
        node->left = deleteNodeValue(node->left, value, compare, isDeleted, errorCode, isBalancingStopped, valueOfDeletedElement);
        if (*isDeleted && !(*isBalancingStopped)) {
            node->balance += 1;
        }
    }

    if (node->balance == -1 || node->balance == 1) {
        *isBalancingStopped = true;
    }

    return balance(node, isBalancingStopped, false);
}

Type deleteValue(AVLTree *avlTree, Type value, int (*compare)(Type, Type)) {
    Type nullResult = {0};
    if (isEmpty(avlTree)) {
        return nullResult;
    }

    int compareResult = (*compare)(avlTree->root->value, value);
    if (compareResult > 1 || compareResult < -1) {
        return nullResult;//случай когда функция compare неправильна
    }

    bool isBalancingStopped = false;
    if (!compareResult) {
        Node *newRoot = deleteRoot(avlTree->root, &isBalancingStopped);
        Type returnValue = avlTree->root->value;
        avlTree->root = newRoot;
        return returnValue;
    }

    bool isDeleted = false;
    int errorCode = 0;
    Type valueOfDeletedElement = {0};
    avlTree->root = deleteNodeValue(avlTree->root, value, compare, &isDeleted, &errorCode, &isBalancingStopped, &valueOfDeletedElement);
    return valueOfDeletedElement;
}

void clearNode(Node *node) {
    if (node == NULL) {
        return;
    }
    clearNode(node->right);
    clearNode(node->left);
    free(node);
}

void clear(AVLTree *avlTree) {
    if (isEmpty(avlTree)) {
        return;
    }
    clearNode(avlTree->root);
    free(avlTree);
}

Type findNodeValue(Node *node, Type value, int *errorCode, Type zeroValue, int (*compare)(Type, Type)) {
    if (node == NULL) {
        return zeroValue;
    }

    int compareResult = (*compare)(node->value, value);
    if (compareResult > 1 || compareResult < -1) {
        *errorCode = 1;
        return zeroValue;//случай когда функция compare неправильна
    }

    if (!compareResult) {
        return node->value;
    }
    if (compareResult == -1) {
        return findNodeValue(node->right, value, errorCode, zeroValue, compare);
    }
    return findNodeValue(node->left, value, errorCode, zeroValue, compare);
}

Type findValue(AVLTree *avlTree, Type value, int *errorCode, Type zeroValue, int (*compare)(Type, Type)) {
    if (isEmpty(avlTree)) {
        return zeroValue;
    }
    return findNodeValue(avlTree->root, value, errorCode, zeroValue, compare);
}