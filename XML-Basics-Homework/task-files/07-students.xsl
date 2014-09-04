<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<body>
				<h1>Students List</h1>
				<table bgcolor="#E0E0E0" cellspacing="1">
					<tr bgcolor="#EEEEEE">
						<td><b>Name</b></td>
						<td><b>Sex</b></td>
						<td><b>Birth Date</b></td>
						<td><b>Phone</b></td>
						<td><b>Email</b></td>
						<td><b>Course</b></td>
						<td><b>Speciality</b></td>
						<td><b>Faculty Number</b></td>
					</tr>
					<xsl:for-each select="/students/student">
					<tr bgcolor="white">
						<td><xsl:value-of select="name"/></td>
						<td><xsl:value-of select="sex"/></td>
						<td><xsl:value-of select="birthDate"/></td>
						<td><xsl:value-of select="phone"/></td>
						<td><xsl:value-of select="email"/></td>
						<td><xsl:value-of select="course"/></td>
						<td><xsl:value-of select="speciality"/></td>
						<td><xsl:value-of select="facultyNumber"/></td>
						<td>
							<table bgcolor="#E0E0E0" cellspacing="1">
								<tr bgcolor="#EEEEEE">
									<td><b>Exam</b></td>
									<td><b>Tutor</b></td>
									<td><b>Score</b></td>
								</tr>
								<xsl:for-each select="current()/exams/exam">
								<tr bgcolor="white">
									<td><xsl:value-of select="name"/></td>
									<td><xsl:value-of select="tutor"/></td>
									<td><xsl:value-of select="score"/></td>
								</tr>
								</xsl:for-each>
              </table>
						</td>
					</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>