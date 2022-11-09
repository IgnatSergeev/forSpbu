cmake_minimum_required(VERSION 3.20.2)
project(forSpbu C)

set(CMAKE_C_STANDARD 99)

add_executable(forSpbu Modules/BinaryTree/customBinaryTree.c Modules/BinaryTree/customBinaryTree.h Modules/BinaryTree/tempForBT.c 26.10/BinSearchTree.c)
