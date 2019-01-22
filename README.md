EM-uNetClient
===========================================================================================

[EM-uNetPi](https://github.com/KONAMI/EM-uNetPi) 向けのネットワーク品質測定クライアント

Unity（2018.2）でビルドすることを前提としている。

ビルドの際に、下記2点、変更する必要があるため、注意すること。

- PlayerSettings の Company Name / Product Name / Package Name 等について、適宜書き換える 
- 測定のためのサーバ・測定結果を収集するサーバは各自で用意する必要があります
	- 品質測定のためのサーバサイドはプログラムは EM-uNetServer で公開していますので[こちら](https://github.com/KONAMI/EM-uNetServer/tree/master/Api)を参考に構築してください
		- 構築したエンドポイントに合わせて、クライアント側のURL書き換えなどが必要になります
			- Assets/EM-uNetClient/ScriptableObjects/FuncObjStaticConfig の ApiUrl 等
