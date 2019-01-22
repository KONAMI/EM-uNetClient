################################################################################

PROJECT_PATH = /share/EM-uNetMiner

################################################################################

default:

################################################################################

CODE_GENERATOR_BIN     = /usr/local/Utf8Json.UniversalCodeGenerator/Utf8Json.UniversalCodeGenerator
CODE_GENERATOR_OUTPUT  = ${PROJECT_PATH}/Assets/Scripts/Utf8JsonGenerated.cs

CODE_GENERATOR_TARGET  = ${PROJECT_PATH}/Assets/EM-uNetPi-Client/EMNS-Api/TechStore
CODE_GENERATOR_TARGET += ,${PROJECT_PATH}/Assets/EM-uNetPi-Client/EMNS-Api/TechInfo
CODE_GENERATOR_TARGET += ,${PROJECT_PATH}/Assets/EM-uNetPi-Client/EMNS-Api/TechAuth
CODE_GENERATOR_TARGET += ,${PROJECT_PATH}/Assets/EM-uNetPi-Client/Scripts/UDPing
CODE_GENERATOR_DEP     = ${PROJECT_PATH}/Assets/EM-uNetPi-Client/EMNS-Api/TechStore/*.cs 
CODE_GENERATOR_DEP    += ${PROJECT_PATH}/Assets/EM-uNetPi-Client/EMNS-Api/TechInfo/*.cs 
CODE_GENERATOR_DEP    += ${PROJECT_PATH}/Assets/EM-uNetPi-Client/EMNS-Api/TechAuth/*.cs 
CODE_GENERATOR_DEP    += ${PROJECT_PATH}/Assets/EM-uNetPi-Client/Scripts/UDPing/*.cs

EMPTY =

${CODE_GENERATOR_OUTPUT}: ${CODE_GENERATOR_DEP}
	${CODE_GENERATOR_BIN} -d "$(subst $() ,,${CODE_GENERATOR_TARGET})" -o "${CODE_GENERATOR_OUTPUT}"

test: 

all: ${CODE_GENERATOR_OUTPUT}

clean:
	rm ${CODE_GENERATOR_OUTPUT}

################################################################################
