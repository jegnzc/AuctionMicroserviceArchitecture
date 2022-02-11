provider "azurerm" {
  features {}
}
resource "azurerm_cosmosdb_account" "cosmos-db" {
  name                = "mongodboas"
  location            = "East US"
  resource_group_name = "cloud-shell-storage-eastus"
  offer_type          = "Standard"
  kind                = "MongoDB"
  consistency_policy {
    consistency_level       = "BoundedStaleness"
    max_interval_in_seconds = 10
    max_staleness_prefix    = 200
  }
  geo_location {
    location          = "East US"
    failover_priority = 0
  }
}
