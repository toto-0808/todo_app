# 対応内容の概要

ドメイン駆動開発の構成ベースでDB処理を中心にリファクタリングを行う

## 対応順序

1. プロジェクトを細分化する
1. ValueObjectを作成
1. ModelをEntityに変更
1. Repositoryパターンを適用する

## プロジェクト構成

- todo_app
- todo_app.Infrastracture
	- EntityFramework
- todo_app.Domain
	- ValueObject
	- Repository

## 参考

EntityFrameworkCore+DDD【C#】