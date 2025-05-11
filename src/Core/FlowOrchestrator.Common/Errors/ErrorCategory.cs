namespace FlowOrchestrator.Common.Errors;

/// <summary>
/// Represents the category of an error.
/// </summary>
public enum ErrorCategory
{
    /// <summary>
    /// The error is related to validation.
    /// </summary>
    Validation,
    
    /// <summary>
    /// The error is related to configuration.
    /// </summary>
    Configuration,
    
    /// <summary>
    /// The error is related to authentication.
    /// </summary>
    Authentication,
    
    /// <summary>
    /// The error is related to authorization.
    /// </summary>
    Authorization,
    
    /// <summary>
    /// The error is related to data access.
    /// </summary>
    DataAccess,
    
    /// <summary>
    /// The error is related to business logic.
    /// </summary>
    BusinessLogic,
    
    /// <summary>
    /// The error is related to external services.
    /// </summary>
    ExternalService,
    
    /// <summary>
    /// The error is related to infrastructure.
    /// </summary>
    Infrastructure,
    
    /// <summary>
    /// The error is related to communication.
    /// </summary>
    Communication,
    
    /// <summary>
    /// The error is related to resource allocation.
    /// </summary>
    ResourceAllocation,
    
    /// <summary>
    /// The error is related to concurrency.
    /// </summary>
    Concurrency,
    
    /// <summary>
    /// The error is related to security.
    /// </summary>
    Security,
    
    /// <summary>
    /// The error is related to performance.
    /// </summary>
    Performance,
    
    /// <summary>
    /// The error is related to compatibility.
    /// </summary>
    Compatibility,
    
    /// <summary>
    /// The error is related to the system.
    /// </summary>
    System,
    
    /// <summary>
    /// The error is related to the user.
    /// </summary>
    User,
    
    /// <summary>
    /// The error is related to the application.
    /// </summary>
    Application,
    
    /// <summary>
    /// The error is related to the network.
    /// </summary>
    Network,
    
    /// <summary>
    /// The error is related to the database.
    /// </summary>
    Database,
    
    /// <summary>
    /// The error is related to the file system.
    /// </summary>
    FileSystem,
    
    /// <summary>
    /// The error is related to memory.
    /// </summary>
    Memory,
    
    /// <summary>
    /// The error is related to the processor.
    /// </summary>
    Processor,
    
    /// <summary>
    /// The error is related to the scheduler.
    /// </summary>
    Scheduler,
    
    /// <summary>
    /// The error is related to the orchestrator.
    /// </summary>
    Orchestrator,
    
    /// <summary>
    /// The error is related to the importer.
    /// </summary>
    Importer,
    
    /// <summary>
    /// The error is related to the exporter.
    /// </summary>
    Exporter,
    
    /// <summary>
    /// The error is related to the processor.
    /// </summary>
    ProcessorComponent,
    
    /// <summary>
    /// The error is related to the manager.
    /// </summary>
    Manager,
    
    /// <summary>
    /// The error is related to the protocol.
    /// </summary>
    Protocol,
    
    /// <summary>
    /// The error is related to the strategy.
    /// </summary>
    Strategy,
    
    /// <summary>
    /// The error is related to the flow.
    /// </summary>
    Flow,
    
    /// <summary>
    /// The error is related to the branch.
    /// </summary>
    Branch,
    
    /// <summary>
    /// The error is related to the step.
    /// </summary>
    Step,
    
    /// <summary>
    /// The error is related to the source.
    /// </summary>
    Source,
    
    /// <summary>
    /// The error is related to the destination.
    /// </summary>
    Destination,
    
    /// <summary>
    /// The error is related to the data.
    /// </summary>
    Data,
    
    /// <summary>
    /// The error is related to the schema.
    /// </summary>
    Schema,
    
    /// <summary>
    /// The error is related to the format.
    /// </summary>
    Format,
    
    /// <summary>
    /// The error is related to the transformation.
    /// </summary>
    Transformation,
    
    /// <summary>
    /// The error is related to the filter.
    /// </summary>
    Filter,
    
    /// <summary>
    /// The error is related to the merge.
    /// </summary>
    Merge,
    
    /// <summary>
    /// The error is related to the split.
    /// </summary>
    Split,
    
    /// <summary>
    /// The error is related to the join.
    /// </summary>
    Join,
    
    /// <summary>
    /// The error is related to the aggregation.
    /// </summary>
    Aggregation,
    
    /// <summary>
    /// The error is related to the enrichment.
    /// </summary>
    Enrichment,
    
    /// <summary>
    /// The error is related to the validation.
    /// </summary>
    ValidationComponent,
    
    /// <summary>
    /// The error is related to the monitoring.
    /// </summary>
    Monitoring,
    
    /// <summary>
    /// The error is related to the logging.
    /// </summary>
    Logging,
    
    /// <summary>
    /// The error is related to the alerting.
    /// </summary>
    Alerting,
    
    /// <summary>
    /// The error is related to the reporting.
    /// </summary>
    Reporting,
    
    /// <summary>
    /// The error is related to the analytics.
    /// </summary>
    Analytics,
    
    /// <summary>
    /// The error is related to the dashboard.
    /// </summary>
    Dashboard,
    
    /// <summary>
    /// The error is related to the UI.
    /// </summary>
    UI,
    
    /// <summary>
    /// The error is related to the API.
    /// </summary>
    API,
    
    /// <summary>
    /// The error is related to the CLI.
    /// </summary>
    CLI,
    
    /// <summary>
    /// The error is related to the SDK.
    /// </summary>
    SDK,
    
    /// <summary>
    /// The error is related to the plugin.
    /// </summary>
    Plugin,
    
    /// <summary>
    /// The error is related to the extension.
    /// </summary>
    Extension,
    
    /// <summary>
    /// The error is related to the integration.
    /// </summary>
    Integration,
    
    /// <summary>
    /// The error is related to the connector.
    /// </summary>
    Connector,
    
    /// <summary>
    /// The error is related to the adapter.
    /// </summary>
    Adapter,
    
    /// <summary>
    /// The error is related to the converter.
    /// </summary>
    Converter,
    
    /// <summary>
    /// The error is related to the parser.
    /// </summary>
    Parser,
    
    /// <summary>
    /// The error is related to the formatter.
    /// </summary>
    Formatter,
    
    /// <summary>
    /// The error is related to the serializer.
    /// </summary>
    Serializer,
    
    /// <summary>
    /// The error is related to the deserializer.
    /// </summary>
    Deserializer,
    
    /// <summary>
    /// The error is related to the encoder.
    /// </summary>
    Encoder,
    
    /// <summary>
    /// The error is related to the decoder.
    /// </summary>
    Decoder,
    
    /// <summary>
    /// The error is related to the compressor.
    /// </summary>
    Compressor,
    
    /// <summary>
    /// The error is related to the decompressor.
    /// </summary>
    Decompressor,
    
    /// <summary>
    /// The error is related to the encryptor.
    /// </summary>
    Encryptor,
    
    /// <summary>
    /// The error is related to the decryptor.
    /// </summary>
    Decryptor,
    
    /// <summary>
    /// The error is related to the hasher.
    /// </summary>
    Hasher,
    
    /// <summary>
    /// The error is related to the verifier.
    /// </summary>
    Verifier,
    
    /// <summary>
    /// The error is related to the validator.
    /// </summary>
    Validator,
    
    /// <summary>
    /// The error is related to the generator.
    /// </summary>
    Generator,
    
    /// <summary>
    /// The error is related to the calculator.
    /// </summary>
    Calculator,
    
    /// <summary>
    /// The error is related to the analyzer.
    /// </summary>
    Analyzer,
    
    /// <summary>
    /// The error is related to the optimizer.
    /// </summary>
    Optimizer,
    
    /// <summary>
    /// The error is related to the cleaner.
    /// </summary>
    Cleaner,
    
    /// <summary>
    /// The error is related to the backup.
    /// </summary>
    Backup,
    
    /// <summary>
    /// The error is related to the restore.
    /// </summary>
    Restore,
    
    /// <summary>
    /// The error is related to the migration.
    /// </summary>
    Migration,
    
    /// <summary>
    /// The error is related to the deployment.
    /// </summary>
    Deployment,
    
    /// <summary>
    /// The error is related to the installation.
    /// </summary>
    Installation,
    
    /// <summary>
    /// The error is related to the uninstallation.
    /// </summary>
    Uninstallation,
    
    /// <summary>
    /// The error is related to the upgrade.
    /// </summary>
    Upgrade,
    
    /// <summary>
    /// The error is related to the downgrade.
    /// </summary>
    Downgrade,
    
    /// <summary>
    /// The error is related to the initialization.
    /// </summary>
    Initialization,
    
    /// <summary>
    /// The error is related to the termination.
    /// </summary>
    Termination,
    
    /// <summary>
    /// The error is related to the startup.
    /// </summary>
    Startup,
    
    /// <summary>
    /// The error is related to the shutdown.
    /// </summary>
    Shutdown,
    
    /// <summary>
    /// The error is related to the restart.
    /// </summary>
    Restart,
    
    /// <summary>
    /// The error is related to the pause.
    /// </summary>
    Pause,
    
    /// <summary>
    /// The error is related to the resume.
    /// </summary>
    Resume,
    
    /// <summary>
    /// The error is related to the cancel.
    /// </summary>
    Cancel,
    
    /// <summary>
    /// The error is related to the timeout.
    /// </summary>
    Timeout,
    
    /// <summary>
    /// The error is related to the retry.
    /// </summary>
    Retry,
    
    /// <summary>
    /// The error is related to the fallback.
    /// </summary>
    Fallback,
    
    /// <summary>
    /// The error is related to the recovery.
    /// </summary>
    Recovery,
    
    /// <summary>
    /// The error is related to the compensation.
    /// </summary>
    Compensation,
    
    /// <summary>
    /// The error is related to the rollback.
    /// </summary>
    Rollback,
    
    /// <summary>
    /// The error is related to the commit.
    /// </summary>
    Commit,
    
    /// <summary>
    /// The error is related to the transaction.
    /// </summary>
    Transaction,
    
    /// <summary>
    /// The error is related to the lock.
    /// </summary>
    Lock,
    
    /// <summary>
    /// The error is related to the unlock.
    /// </summary>
    Unlock,
    
    /// <summary>
    /// The error is related to the synchronization.
    /// </summary>
    Synchronization,
    
    /// <summary>
    /// The error is related to the asynchronization.
    /// </summary>
    Asynchronization,
    
    /// <summary>
    /// The error is related to the parallelization.
    /// </summary>
    Parallelization,
    
    /// <summary>
    /// The error is related to the serialization.
    /// </summary>
    Serialization,
    
    /// <summary>
    /// The error is related to the deserialization.
    /// </summary>
    Deserialization,
    
    /// <summary>
    /// The error is related to the marshalling.
    /// </summary>
    Marshalling,
    
    /// <summary>
    /// The error is related to the unmarshalling.
    /// </summary>
    Unmarshalling,
    
    /// <summary>
    /// The error is related to the encoding.
    /// </summary>
    Encoding,
    
    /// <summary>
    /// The error is related to the decoding.
    /// </summary>
    Decoding,
    
    /// <summary>
    /// The error is related to the compression.
    /// </summary>
    Compression,
    
    /// <summary>
    /// The error is related to the decompression.
    /// </summary>
    Decompression,
    
    /// <summary>
    /// The error is related to the encryption.
    /// </summary>
    Encryption,
    
    /// <summary>
    /// The error is related to the decryption.
    /// </summary>
    Decryption,
    
    /// <summary>
    /// The error is related to the hashing.
    /// </summary>
    Hashing,
    
    /// <summary>
    /// The error is related to the verification.
    /// </summary>
    Verification,
    
    /// <summary>
    /// The error is related to the validation.
    /// </summary>
    ValidationProcess,
    
    /// <summary>
    /// The error is related to the generation.
    /// </summary>
    Generation,
    
    /// <summary>
    /// The error is related to the calculation.
    /// </summary>
    Calculation,
    
    /// <summary>
    /// The error is related to the analysis.
    /// </summary>
    Analysis,
    
    /// <summary>
    /// The error is related to the optimization.
    /// </summary>
    Optimization,
    
    /// <summary>
    /// The error is related to the cleaning.
    /// </summary>
    Cleaning,
    
    /// <summary>
    /// The error is related to the backup.
    /// </summary>
    BackupProcess,
    
    /// <summary>
    /// The error is related to the restore.
    /// </summary>
    RestoreProcess,
    
    /// <summary>
    /// The error is related to the migration.
    /// </summary>
    MigrationProcess,
    
    /// <summary>
    /// The error is related to the deployment.
    /// </summary>
    DeploymentProcess,
    
    /// <summary>
    /// The error is related to the installation.
    /// </summary>
    InstallationProcess,
    
    /// <summary>
    /// The error is related to the uninstallation.
    /// </summary>
    UninstallationProcess,
    
    /// <summary>
    /// The error is related to the upgrade.
    /// </summary>
    UpgradeProcess,
    
    /// <summary>
    /// The error is related to the downgrade.
    /// </summary>
    DowngradeProcess,
    
    /// <summary>
    /// The error is related to the initialization.
    /// </summary>
    InitializationProcess,
    
    /// <summary>
    /// The error is related to the termination.
    /// </summary>
    TerminationProcess,
    
    /// <summary>
    /// The error is related to the startup.
    /// </summary>
    StartupProcess,
    
    /// <summary>
    /// The error is related to the shutdown.
    /// </summary>
    ShutdownProcess,
    
    /// <summary>
    /// The error is related to the restart.
    /// </summary>
    RestartProcess,
    
    /// <summary>
    /// The error is related to the pause.
    /// </summary>
    PauseProcess,
    
    /// <summary>
    /// The error is related to the resume.
    /// </summary>
    ResumeProcess,
    
    /// <summary>
    /// The error is related to the cancel.
    /// </summary>
    CancelProcess,
    
    /// <summary>
    /// The error is related to the timeout.
    /// </summary>
    TimeoutProcess,
    
    /// <summary>
    /// The error is related to the retry.
    /// </summary>
    RetryProcess,
    
    /// <summary>
    /// The error is related to the fallback.
    /// </summary>
    FallbackProcess,
    
    /// <summary>
    /// The error is related to the recovery.
    /// </summary>
    RecoveryProcess,
    
    /// <summary>
    /// The error is related to the compensation.
    /// </summary>
    CompensationProcess,
    
    /// <summary>
    /// The error is related to the rollback.
    /// </summary>
    RollbackProcess,
    
    /// <summary>
    /// The error is related to the commit.
    /// </summary>
    CommitProcess,
    
    /// <summary>
    /// The error is related to the transaction.
    /// </summary>
    TransactionProcess,
    
    /// <summary>
    /// The error is related to the lock.
    /// </summary>
    LockProcess,
    
    /// <summary>
    /// The error is related to the unlock.
    /// </summary>
    UnlockProcess,
    
    /// <summary>
    /// The error is related to the synchronization.
    /// </summary>
    SynchronizationProcess,
    
    /// <summary>
    /// The error is related to the asynchronization.
    /// </summary>
    AsynchronizationProcess,
    
    /// <summary>
    /// The error is related to the parallelization.
    /// </summary>
    ParallelizationProcess,
    
    /// <summary>
    /// The error is related to the serialization.
    /// </summary>
    SerializationProcess,
    
    /// <summary>
    /// The error is related to the deserialization.
    /// </summary>
    DeserializationProcess,
    
    /// <summary>
    /// The error is related to the marshalling.
    /// </summary>
    MarshallingProcess,
    
    /// <summary>
    /// The error is related to the unmarshalling.
    /// </summary>
    UnmarshallingProcess,
    
    /// <summary>
    /// The error is related to the encoding.
    /// </summary>
    EncodingProcess,
    
    /// <summary>
    /// The error is related to the decoding.
    /// </summary>
    DecodingProcess,
    
    /// <summary>
    /// The error is related to the compression.
    /// </summary>
    CompressionProcess,
    
    /// <summary>
    /// The error is related to the decompression.
    /// </summary>
    DecompressionProcess,
    
    /// <summary>
    /// The error is related to the encryption.
    /// </summary>
    EncryptionProcess,
    
    /// <summary>
    /// The error is related to the decryption.
    /// </summary>
    DecryptionProcess,
    
    /// <summary>
    /// The error is related to the hashing.
    /// </summary>
    HashingProcess,
    
    /// <summary>
    /// The error is related to the verification.
    /// </summary>
    VerificationProcess,
    
    /// <summary>
    /// The error is related to the validation.
    /// </summary>
    ValidationProcessing,
    
    /// <summary>
    /// The error is related to the generation.
    /// </summary>
    GenerationProcess,
    
    /// <summary>
    /// The error is related to the calculation.
    /// </summary>
    CalculationProcess,
    
    /// <summary>
    /// The error is related to the analysis.
    /// </summary>
    AnalysisProcess,
    
    /// <summary>
    /// The error is related to the optimization.
    /// </summary>
    OptimizationProcess,
    
    /// <summary>
    /// The error is related to the cleaning.
    /// </summary>
    CleaningProcess,
    
    /// <summary>
    /// The error is unknown.
    /// </summary>
    Unknown
}
