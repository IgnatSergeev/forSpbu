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

int addNode(BinaryTree *tree, int value) {
    if (isEmpty(tree)) {
        Node *root = malloc(sizeof(Node));
        root->left = NULL;
        root->right = NULL;
        root->value = value;
        tree->root = root;
        return 0;
    }

    Node *currentRoot = tree->root;
    if (currentRoot->value == value) {
        return -1;
    }

    BinaryTree *newTree = create();
    if (newTree == NULL) {
        return -1;
    }

    if (currentRoot->value < value) {
        newTree->root = currentRoot->right;
        if (addNode(newTree, value)) {
            free(newTree);
            return -1;
        }
        currentRoot->right = newTree->root;
        free(newTree);
        return 0;
    } else {
        newTree->root = currentRoot->left;
        if (addNode(newTree, value)) {
            free(newTree);
            return -1;
        }
        currentRoot->left = newTree->root;
        free(newTree);
        return 0;
    }
}

bool isEmpty(BinaryTree *tree) {
    return tree->root == NULL;
}

int findNode(BinaryTree *tree, int value, int *errorCode) {
    if (isEmpty(tree)) {
        *errorCode = 1;
        return 0;
    }

    Node *currentRoot = tree->root;
    if (currentRoot->value == value) {
        return value;
    }

    BinaryTree *newTree = create();
    if (newTree == NULL) {
        *errorCode = 1;
        return 0;
    }

    if (currentRoot->value < value) {
        newTree->root = currentRoot->right;
    } else {
        newTree->root = currentRoot->left;
    }
    int returnValue = findNode(newTree, value, errorCode);
    free(newTree);
    if (*errorCode) {
        return 0;
    }
    return returnValue;
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