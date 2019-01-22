EM-uNetClient
===========================================================================================

[EM-uNetPi](https://github.com/KONAMI/EM-uNetPi) 向けのネットワーク品質測定クライアント

Unity（2018.2）でビルドすることを前提としている。

ビルドの際に、下記2点、変更する必要があるため、注意すること。

- PlayerSettings の Company Name / Product Name / Package Name 等について、適宜書き換える 
- Assets/EM-uNetClient/ScriptableObjects/FuncObjStaticConfig の ApiUrl について、EM-uNetServer/Api で構成した TechInfo の URL に置き換えること
 
