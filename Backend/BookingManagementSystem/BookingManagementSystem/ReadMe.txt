Booking Management System
📖 Overview

This is a simple Booking Management System built using ASP.NET Core Web API with a frontend in Angular.
The system allows users to create bookings for resources, prevents overlapping bookings, retrieve booking data, and cancel bookings using a soft delete approach.

⚙️ Tech Stack
ASP.NET Core Web API (.NET 8)
Entity Framework Core
SQL Server
Angular (Frontend)
xUnit (Unit Testing)
CORS enabled for frontend communication
📌 Features
1. Create Booking
Creates a booking between a user and a resource.
Each booking includes:
ResourceId
UserId
StartDateTime (UTC)
EndDateTime (UTC)
2. Prevent Overlapping Bookings

To ensure no double booking for the same resource, the following logic is applied:

StartDateTime < Existing EndDateTime
AND
EndDateTime > Existing StartDateTime

This ensures that no two bookings overlap in time for the same resource.

3. Retrieve Bookings
Get all active bookings.
Get bookings by ResourceId.
Only bookings with status Active are returned by default.
4. Cancel Booking (Soft Delete)
Booking is not physically deleted.
Instead:
Status is updated to Cancelled
CancelledAt timestamp is stored
Cancelled bookings are excluded from active queries.
🧠 Design Decisions
📌 A) Concurrency Handling

The system currently relies on application-level validation to prevent overlapping bookings.
However, in a high-concurrency environment, race conditions may occur if two requests are processed simultaneously.

A stronger solution would include:

Database-level constraints
Transactions or locking mechanisms
Optimistic concurrency control
📌 B) Scalability Considerations

The current design works well for small to medium datasets.
However, as data grows:

Overlap checks may become slower due to full table scans
Database becomes a bottleneck

Possible improvements:

Indexing on ResourceId + StartDateTime + EndDateTime
Caching availability queries (Redis)
Separating read/write models (CQRS)
📌 C) Trade-offs

The design prioritizes:

✔ Simplicity
✔ Correctness of booking logic

Over:

❌ High-performance optimization

This makes the system easier to maintain and extend but may require optimization under heavy load.

🌍 Future Improvements
Add authentication & authorization (JWT)
Add role-based access (Admin/User)
Add recurring bookings
Add availability search feature
Improve concurrency safety at database level
🧪 Testing

Unit tests cover:

Booking creation success
Overlap prevention logic
Booking cancellation flow

Tests are implemented using xUnit with in-memory database.

🚀 How to Run
Update connection string in appsettings.json
Run migrations:
dotnet ef database update
Start backend:
dotnet run
Start Angular frontend:
ng serve


Developed as a backend engineering take-home assignment.