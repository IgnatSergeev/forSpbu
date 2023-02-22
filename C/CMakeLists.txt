cmake_minimum_required(VERSION 3.20.2)
project(forSpbu C)

set(CMAKE_C_STANDARD 99)

add_executable(forSpbu  Modules/Graph/graph.c Modules/Graph/graph.h Modules/Graph/typeDef.h Modules/Graph/queueForGraph.c)
