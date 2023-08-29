set (SRC_ROOT ${PROJECT_SOURCE_DIR}/sources)

set (TARGET_SRC
    ${SRC_ROOT}/main.cpp
)

add_executable(${PROJECT_NAME} ${TARGET_SRC})

set_target_properties(${PROJECT_NAME} PROPERTIES
    RUNTIME_OUTPUT_DIRECTORY ${PROJECT_SOURCE_DIR}
)
