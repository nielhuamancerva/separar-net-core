CREATE TABLE outbox(
  outbox_id BIGINT(20) NOT NULL AUTO_INCREMENT,
  message_id VARCHAR(255) NOT NULL,
  dispatched TINYINT(1) NOT NULL,
  dispatched_at DATETIME DEFAULT NULL,
  transport_operations VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (outbox_id),
  UNIQUE INDEX UQ_outbox_message_id(message_id),
  INDEX IX_outbox_dispatched(dispatched),
  INDEX IX_outbox_dispatched_at(dispatched_at)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TABLE MoneyTransferSagaData (
  Id VARCHAR(40) NOT NULL,
  Originator VARCHAR(255) DEFAULT NULL,
  OriginalMessageId VARCHAR(255) DEFAULT NULL,
  TransferId VARCHAR(255) DEFAULT NULL,
  SourceAccountId VARCHAR(255) DEFAULT NULL,
  DestinationAccountId VARCHAR(255) DEFAULT NULL,
  Amount DECIMAL(19, 5) DEFAULT NULL,
  PRIMARY KEY (Id),
  UNIQUE INDEX UQ_MoneyTransferSagaData_TransferId(TransferId)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TABLE transfer(
  transfer_id VARCHAR(36) NOT NULL,
  source_account_id VARCHAR(36) NOT NULL,
  destination_account_id VARCHAR(36) NOT NULL,
  amount DECIMAL(10,2) UNSIGNED NOT NULL,
  transfer_state_id TINYINT UNSIGNED NOT NULL,
  started_at_utc DATETIME NOT NULL,
  updated_at_utc DATETIME NOT NULL,
  PRIMARY KEY(transfer_id)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;