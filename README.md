EM-uNetClient
===========================================================================================

[EM-uNetPi](https://github.com/KONAMI/EM-uNetPi) 向けのネットワーク品質測定クライアント

Unity（2018.2）でビルドすることを前提としている。

ビルドの際に、下記2点、変更する必要があるため、注意すること。

- PlayerSettings の Company Name / Product Name / Package Name 等について、適宜書き換える 
- Assets/EM-uNetClient/ScriptableObjects/FuncObjStaticConfig の ApiUrl について、[EM-uNetServer/Api](https://github.com/KONAMI/EM-uNetServer/tree/master/Api) で構成した TechInfo の URL に置き換えること

> 品質測定のためのサーバサイドはプログラムは公開されているが、標準で利用な可能なものは存在しない。そのため、クライアントに対応したAPI群を各々で構築する必要がある。
