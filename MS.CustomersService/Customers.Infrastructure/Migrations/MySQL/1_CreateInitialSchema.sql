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

CREATE TABLE customer(
  customer_id VARCHAR(36) NOT NULL,
  first_name VARCHAR(50) NOT NULL,
  last_name VARCHAR(50) NOT NULL,
  identity_document VARCHAR(8) NOT NULL,
  active BIT NOT NULL,
  created_at_utc DATETIME NOT NULL,
  updated_at_utc DATETIME NOT NULL,
  PRIMARY KEY(customer_id),
  INDEX IX_customer_last_first_name(last_name, first_name),
  UNIQUE INDEX UQ_customer_identity_document(identity_document),
  FULLTEXT INDEX FT_customer_first_last_name(first_name,last_name)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;