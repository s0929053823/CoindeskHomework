# Coindesk Homework

這是一個基於 .NET 8 的 Web API 項目，旨在展示如何構建簡單的 API 並進行資料處理與交換。該 API 可以處理貨幣資料，提供對外部 API 的整合，並在內部進行資料的增刪查改操作。

## 目錄

- [功能](#功能)
- [環境需求](#環境需求)
- [安裝步驟](#安裝步驟)
- [使用方式](#使用方式)
- [單元測試](#單元測試)


## 功能

- 提供貨幣資料查詢。
- 整合 Coindesk API，獲取即時匯率資料。
- 實現貨幣資料的增刪查改功能。

## 環境需求

- .NET 8.0 或更高版本
- SQL Server / SQL Server Express 或其他支援的資料庫

## 安裝步驟

執行專案：
    ```bash
    dotnet run
    ```

## 使用方式

- 當你成功啟動專案後，你可以訪問以下端點：
    - `GET /api/currency` - 獲取所有貨幣資料。
    - `GET /api/currency/{id}` - 根據 ID 獲取特定貨幣資料。
    - `POST /api/currency` - 創建新貨幣資料。
    - `PUT /api/currency/{id}` - 更新指定貨幣資料。
    - `DELETE /api/currency/{id}` - 刪除指定貨幣資料。

## Swagger 檔案

API 端點的詳細文檔會自動生成並可以通過 Swagger UI 訪問

## 單元測試
使用 xUnit 進行單元測試

## 錯誤處理
呼叫API發生錯誤均會記錄在Db的ApiLog資料表
