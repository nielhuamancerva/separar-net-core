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

CREATE TABLE account(
  account_id VARCHAR(36) NOT NULL,
  number VARCHAR(50) NOT NULL,
  balance DECIMAL(10,2) UNSIGNED NOT NULL,
  customer_id VARCHAR(36) NOT NULL,
  account_state_id TINYINT UNSIGNED NOT NULL,
  opened_at_utc DATETIME NOT NULL,
  updated_at_utc DATETIME NOT NULL,
  PRIMARY KEY(account_id),
  UNIQUE INDEX UQ_account_number(number)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;