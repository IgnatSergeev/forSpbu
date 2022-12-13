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
              int *errorCode, bool *isAdded, bool *isBalanced, bool *isBalancingStopped) {
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
        node->right = addNode(node->right, value, compare, whatIfEqualInAdding, errorCode, isAdded, isBalanced, isBalancingStopped);

        if (*isAdded && !(*isBalancingStopped)) {
            ++node->balance;
        }
    }
    if (compareResult == 1) {
        node->left = addNode(node->left, value, compare, whatIfEqualInAdding, errorCode, isAdded, isBalanced, isBalancingStopped);

        if (*isAdded && !(*isBalancingStopped)) {
            --node->balance;
        }
    }

    if (node->balance == 0 || node->balance == 2 || node->balance == -2) {
        *isBalancingStopped = true;
    }

    return balance(node, isBalanced);
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
    bool isBalancingStopped = false;
    avlTree->root = addNode(avlTree->root, value, compare, whatIfEqualInAdding, &errorCode, &isAdded, &isBalanced, &isBalancingStopped);
    return errorCode;
}

Node *findAndCutOutBiggestNodeInLeftSonTree(Node *node, bool *isBalanced, Node **nodeForSearch) {
    if (node->right == NULL) {

        *nodeForSearch = node;
        return node->left;
    }
    node->right = findAndCutOutBiggestNodeInLeftSonTree(node->right, isBalanced, nodeForSearch);
    if (!(*isBalanced)) {
        node->balance -= 1;
    }

    return balance(node, isBalanced);
}

Node *deleteRoot(Node *root, bool *isBalanced) {
    if (root->left == NULL) {
        Node *newRoot = root->right;
        free(root);
        return newRoot;
    }
    Node **nodeForSearch = malloc(sizeof(Node *));
    root->left = findAndCutOutBiggestNodeInLeftSonTree(root->left, isBalanced, nodeForSearch);
    Node *newRoot = *nodeForSearch;
    if (!(*isBalanced)) {
        root->balance += 1;
    }

    newRoot->balance = root->balance;
    newRoot->right = root->right;
    newRoot->left = root->left;
    free(root);
    newRoot = balance(newRoot, isBalanced);
    return newRoot;
}

Node *deleteNodeValue(Node *node, Type value, int (*compare)(Type, Type), bool *isBalanced, bool *isDeleted, int *errorCode, bool *isBalancingStopped) {
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
        Node *newNode = deleteRoot(node, isBalanced);
        return newNode;
    }
    if (compareResult == -1) {
        node->right = deleteNodeValue(node->right, value, compare, isBalanced, isDeleted, errorCode, isBalancingStopped);
        if (*isDeleted && !(*isBalancingStopped)) {
            node->balance -= 1;
        }
    } else {
        node->left = deleteNodeValue(node->left, value, compare, isBalanced, isDeleted, errorCode, isBalancingStopped);
        if (*isDeleted && !(*isBalancingStopped)) {
            node->balance += 1;
        }
    }

    if (node->balance == -1 || node->balance == 1) {
        *isBalancingStopped = true;
    }

    return balance(node, isBalanced);
}

int deleteValue(AVLTree *avlTree, Type value, int (*compare)(Type, Type)) {
    if (isEmpty(avlTree)) {
        return 0;
    }

    int compareResult = (*compare)(avlTree->root->value, value);
    if (compareResult > 1 || compareResult < -1) {
        return -1;//случай когда функция compare неправильна
    }

    bool isBalanced = false;
    if (!compareResult) {
        Node *newRoot = deleteRoot(avlTree->root, &isBalanced);
        avlTree->root = newRoot;
        return 0;
    }

    bool isDeleted = false;
    int errorCode = 0;
    bool isBalancingStopped = false;
    avlTree->root = deleteNodeValue(avlTree->root, value, compare, &isBalanced, &isDeleted, &errorCode, &isBalancingStopped);
    return errorCode;
}