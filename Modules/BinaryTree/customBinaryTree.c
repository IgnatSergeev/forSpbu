#include "customBinaryTree.h"
#include <stdbool.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>

typedef struct Node {
    int value;
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

Node *addNode(Node *node, int value, int *errorCode) {
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
    if (node->value == value) {
        return node;
    }
    if (node->value < value) {
        node->right = addNode(node->right, value, errorCode);
        return node;

    }
    node->left = addNode(node->left, value, errorCode);
    return node;
}

int addValue(BinaryTree *tree, int value) {
    int errorCode = 0;
    tree->root = addNode(tree->root, value, &errorCode);
    return errorCode;
}

bool isEmpty(BinaryTree *tree) {
    return tree->root == NULL;
}

int findNodeValue(Node *node, int value, int *errorCode) {
    *errorCode = 0;
    if (node == NULL) {
        *errorCode = 1;
        return 0;
    }
    if (node->value == value) {
        return node->value;
    }
    if (node->value < value) {
        return findNodeValue(node->right, value, errorCode);
    }
    return findNodeValue(node->left, value, errorCode);
}

int findValue(BinaryTree *tree, int value, int *errorCode) {
    return findNodeValue(tree->root, value, errorCode);
}

BinaryTree *create() {
    BinaryTree *binaryTree = malloc(sizeof(BinaryTree));
    if (binaryTree == NULL) {
        return NULL;
    }
    binaryTree->root = NULL;

    return binaryTree;
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

void deleteNodeValue(Node *parent, enum Direction dir, Node *node, int value) {
    if (node == NULL) {
        return;
    }
    if (node->value == value) {
        Node *newNode = deleteRoot(node);
        if (dir == right) {
            parent->right = newNode;
        } else {
            parent->left = newNode;
        }
        return;
    }
    if (node->value < value) {
        deleteNodeValue(node, right, node->right, value);
    }
    deleteNodeValue(node, left, node->left, value);
}

void deleteValue(BinaryTree *tree, int value) {
    if (isEmpty(tree)) {
        return;
    }
    if (tree->root->value == value) {
        Node *newRoot = deleteRoot(tree->root);
        tree->root = newRoot;
        return;
    }
    deleteNodeValue(NULL, right, tree->root, value);
}