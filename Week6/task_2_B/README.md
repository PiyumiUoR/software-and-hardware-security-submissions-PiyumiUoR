# How to exploit Mifare classic card technology

* [Introduction](#Introduction)
* [MIFARE Classic Technology Overview](#MIFARE-Classic-Technology-Overview)
* [Reader Module](#Reader-Module)
* [MIFARE Classic Vulnerabilities](#MIFARE-Classic-Vulnerabilities)
* [Exploiting MIFARE Classic](#Exploiting-MIFARE-Classic)
* [Mitigating MIFARE Classic Vulnerabilities](#Mitigating-MIFARE-Classic-Vulnerabilities)
* [Conclusion](#Conclusion)


**Abstract**

This study exposes significant vulnerabilities in the MIFARE Classic RFID system, a widely used technology for access control and transportation. The weaknesses stem from a weak pseudorandom generator, susceptibility to known plaintext attacks, nonce generation issues, and potential brute force attacks, even though the cryptographic algorithm remains undisclosed. These vulnerabilities could be exploited with relatively inexpensive hardware and software. Mitigating these issues involves transitioning to more secure RFID technologies, regular encryption key updates, robust access controls, strong authentication mechanisms, continuous system updates, and comprehensive monitoring. In the long term, migrating to open-design RFID systems is recommended, as they offer transparency and enhanced security. The MIFARE Classic RFID system's vulnerabilities put sensitive data, data integrity, and key security at risk. This underscores the need for advanced RFID technologies and robust security practices to counter evolving threats effectively.

## Introduction

The well-known passive RFID chip brand MIFARE, from MIkron FARE Collection System, is manufactured by NXP and is typically used in RFID cards and tags with a read/write distance of 10 cm (4 inches). MIFARE is compact and adaptable enough to be installed almost anyplace, including key fobs, RFID wristbands, plastic cards, and even cell phones. It can therefore assist people with almost everything. Items such as bus passes, employment badges, metro passes, library cards, student IDs, loyalty cards, toll cards, stadium admission passes, and a growing number of cellphones already contain it. Over 70 nations and over 1.2 billion users have access to MIFARE-based solutions. MIFARE products are without a doubt regarded as the industry leader since they are more dependable and proven than any other interface technology available. They are compliant with ISO/IEC 14443, an international standard that is now utilized by over 80% of contactless smart cards.

## MIFARE Classic Technology Overview

The MIFARE Classic card is an RFID technology-based smart card widely used for access control, transportation systems, and more. It follows the ISO 14443 standard, which outlines the communication for contactless integrated circuit cards. The MIFARE Classic consists of key components:

1. **ISO Standard**: ISO 14443 is the foundation for MIFARE Classic's communication. It has four parts:
   - Part 1 specifies the physical characteristics and operating conditions.
   - Part 2 defines the communication between the reader and the card, where MIFARE Classic uses Type A modulation.
   - Part 3 describes initialization and anticollision protocols to select a card in the presence of multiple cards.
   - Part 4 outlines command transmission, where MIFARE Classic diverges from the ISO standard with a proprietary protocol.

2. **Logical Structure**: MIFARE Classic is primarily a memory card, organized into data blocks of 16 bytes, grouped into sectors. The 1k version has 16 sectors with 4 data blocks each. The 4k version has 32 sectors with 4 data blocks and 8 sectors with 16 data blocks. Each sector ends with a sector trailer containing keys A and B for authentication, access conditions, and an extra data byte (U). Special data, like the unique identifier (UID), is stored in the first data block.

3. **Commands**: MIFARE Classic has a limited command set. Authentication is needed for actions in a sector, with access conditions controlling operations. Commands include Read, Write, Decrement, Increment, Restore, and Transfer. Value blocks store signed 4-byte values.

4. **Security Features**: MIFARE Classic uses a 48-bit symmetric key for authentication, stored in the sector trailer. It employs a mutual three-pass authentication protocol, based on ISO 9798-2 but with some differences. The communication is encrypted using the proprietary stream cipher CRYPTO1.

## Reader Module

Being developed based on NXP's transponder IC, HF RFID Module is a MIFARE OEM reader/writer. It supports MIFARE Classic® 1K, MIFARE Classic® 4K, MIFARE Ultralight® and is applicable for 13.56MHz. This embedded module does auto Real-time detecting tag which moves into or out of detective range and reports through one output pin's logic level. In addition, it integrates all necessary components and antenna into one PCB. The external Micro-controller can work with SL025M to read/write MIFARE cards by simple serial communication commands.

## MIFARE Classic Vulnerabilities

The cryptographic algorithm's secrecy has always provided protection for the MIFARE Classic card. Although obtaining the cryptographic algorithm necessitates a deep understanding, research teams have now succeeded in doing so by reverse engineering the MIFARE Classic chip. Even if the method is known, using it in an exploit still takes a high level of skill. In the meantime, interested parties can now access certain portions of the attack software and the attack equipment design. As a result, there is now virtually little barrier to attack. The degree of security offered by the card features has been significantly reduced by all of these research endeavors. An attacker might theoretically launch these exploits with some inexpensive hardware (less than €100) and a regular laptop or desktop computer.

The provided text highlights several weaknesses in the MIFARE Classic RFID system:

1. **Weak Pseudo-random Generator**: The system relies on a weak pseudorandom generator on the card to initiate the encryption algorithm, known as CRYPTO1. This weakness was partially exploited by previous researchers.

2. **Known Plaintext Attack**: Researchers extended prior work by demonstrating a practical known plaintext attack. They exploited the weak pseudorandom generator and utilized this knowledge to mount a successful known plaintext attack, a significant security weakness.

3. **Nonce Generation Weakness**: The card's pseudorandom generator was found to have a weakness, enabling attackers to request a large number of card nonces. This is concerning because nonce reuse compromises security. Nonces reappeared within a short time frame, making it easier for attackers to intercept and reuse them.

4. **Online Brute Force Attack**: Without knowledge of the cryptographic algorithm used in MIFARE Classic, attackers can only resort to an online brute force attack on the encryption key. This approach involves a significant time delay due to the system's communication speed.

5. **Exhaustive Key Search**: The text indicates that an exhaustive key search, given the communication delay, would take an impractically long time (16,289,061 days or about 44,627 years) to discover the encryption key.

6. **Off-Line Brute Force Attack with Known Plaintext**: If the cryptographic algorithm is known, attackers can perform an off-line brute force attack with eavesdropped traces from an authentication run. This attack can be accelerated with dedicated hardware, costing around $17,000 and taking only about an hour to compromise the key. This underscores a significant vulnerability in the system, especially when an attacker has access to the known plaintext.

In summary, MIFARE Classic exhibits notable weaknesses in its pseudorandom generator, nonce generation, susceptibility to known plaintext attacks, and the potential for brute force attacks, which pose serious security concerns, especially in scenarios where attackers have the capability and resources to exploit these vulnerabilities.


## Exploiting MIFARE Classic

### Use of hardware

The RFID system consists of a transponder (RFID card) and a reader. The reader includes a radio frequency module, a control unit, and a coupling element, while the card comprises a coupling element and a microchip. The control unit in a MIFARE Classic reader is typically a MIFARE microchip with a closed design. This microchip communicates with application software and executes commands. It handles command modulation, not the application software. The microchip in the card and the communication protocol between the card and reader have closed designs, limiting access to their internal workings. The Proxmark III device, developed by Jonathan Westhues, is used for evaluating the security properties of MIFARE systems. It can eavesdrop on transactions, emulate a MIFARE reader, and communicate with MIFARE cards. It supports both low-frequency (125 kHz - 134 kHz) and high-frequency (13.56 MHz) signal processing. The Proxmark III employs flexible Digital Signal Processing (DSP) that can be customized for specific protocols, allowing it to filter signals and handle (de)modulation. The Proxmark III's software implementation enables eavesdropping on RFID tag-reader communication, emulation of both tags and readers, with a specific focus on MIFARE Classic cards. To support MIFARE Classic, the physical layer of the card's communication follows the ISO14443-A standard. This required the implementation of ISO14443-A functionality, including processing and generating reader-to-tag and tag-to-reader communication. Functions like 'hi14asnoop' for trace collection, 'hi14areader' for reader emulation, and 'hi14asim' for card simulation were added. Custom parity bits were incorporated to support encryption and other security aspects.

In summary, the text outlines the tools and methods used to explore and potentially exploit MIFARE Classic RFID systems, highlighting the need for specialized hardware and software to analyze and interact with these systems, including eavesdropping, emulation, and replay attacks.

### Attack

The attack takes advantage of flaws in the pseudo-random generator of the MIFARE Classic. Both having a real card and having control over the reader are necessary. In order to obtain the keystream that was used in the communication between the reader and the card, the attacker listens in on a previous transaction. Several capabilities are made possible by keystream recovery:
   - Known Plaintext for Brute Force Attack: To begin a brute force attack on the encryption key, the attacker gathers the necessary known plaintext.
   - Gaining Understanding of Byte Commands: The attacker learns about the byte commands that are utilized during communication.
   - Reading Card Contents: Without knowing the encryption key, the attacker can read card contents using the recovered keystream.
   - Modifying Card Contents: Without the encryption key, an attacker can also change the card's data.

Bitwise XOR is used in data encryption, and the keystream shifts according to the message's bit count. The mapping of the recovered keystream to new message sequences, however, presents challenges, particularly with regard to parity bits and the requirement for keystream bit synchronization.

Data encryption uses bitwise XOR, where the keystream shifts based on the bit count of the message. However, there are difficulties in mapping the recovered keystream to new message sequences, especially when it comes to parity bits and the need for keystream bit synchronization.

It is demonstrated that, in the absence of the encryption key, an attacker can read Sector 0 of a MIFARE Classic card. It takes just one transaction for a card and a legitimate reader. Every MIFARE Classic card has known memory contents, and access conditions that can be recovered from manufacturer data make this possible. The attacker can discern whether key B is accessible and, if not, that the plaintext is zeros, exposing the contents of the sector, by comprehending these access conditions.

## Mitigating MIFARE Classic Vulnerabilities

There are some ways to mitigate vulnerabilities in MIFARE Classic RFID systems. Although these steps help lessen some vulnerabilities, no system is impenetrable from attacks. Security should be tackled from all angles, taking into account procedural as well as technical considerations, and tailored to the unique requirements and hazards of the RFID application. Furthermore, maintaining RFID system security requires adhering to industry standards and best practices.

1. One of the most effective approaches is to transition to more secure RFID technologies, such as MIFARE DESFire or MIFARE Plus. These technologies offer stronger encryption and better resistance to attacks.
2. Regularly change encryption keys to minimize the risk of successful attacks and implement robust encryption algorithms to protect data during communication.
3. Implement ACLs to specify which users or devices have access to specific sectors on the card and restrict read access to the necessary sectors only.
4. Require both the reader and the card to authenticate each other before data transfer and utilize challenge-response mechanisms with strong nonces and random data.
5. Regularly update reader firmware and software to patch known vulnerabilities and take advantage of security improvements. 
6. Implement a monitoring system that detects unusual or unauthorized activities, such as multiple failed authentication attempts or repeated transactions. 
7. Implement additional security measures on the card side such as adding PIN protection for reading certain sectors or initiating specific commands and employing on-card encryption for sensitive data stored on the card.
8. Use PKI-based authentication to strengthen the security of card-reader interactions.

9. Implement physical security measures to protect cards and readers from unauthorized access, tampering, or skimming.

10. Train staff and users to recognize and report suspicious activities or tampering attempts.

11. Conduct security assessments, including penetration testing and code reviews, to identify vulnerabilities and areas for improvement.

12. Whenever possible, opt for open standards rather than proprietary protocols to enhance transparency and security.

13. If possible, segment sensitive data into different sectors and enforce strict access controls based on the principle of least privilege.

In this attack, for short-term improvements in MIFARE Classic security, it's advisable not to store sensitive information in sector zero, making key B readable for random data. Avoid storing critical data in the first 6 bytes of any sector. Employ multiple sector authentications in a single transaction, which can hinder attackers aiming to recover plaintext. If feasible, consider using an alternative encryption scheme like AES in the back office and store only encrypted data on the RFID tags. To prevent unauthorized data block modification, add an additional authentication layer, verified in the backoffice. Strong fraud detection mechanisms and enhanced security features in the backoffice are vital to detect and mitigate these types of attacks. Overall, the backoffice systems play a crucial role as a secondary line of defense.

However, in the long term, these countermeasures may prove inadequate. Given the closed design of MIFARE Classic, it's important to recognize that relying on security through obscurity is inherently risky. As a result, a migration to more advanced RFID cards with open design architectures is strongly recommended to enhance security and resilience against future vulnerabilities.

## Conclusion
The authors of this study successfully executed an attack on the MIFARE Classic RFID system, revealing significant vulnerabilities. They employed a MIFARE Classic reader and an unmodified "blank" card with default keys to recover byte-level commands utilized in the proprietary protocol. By acquiring these commands and a sufficiently long keystream, they gained the ability to perform actions as if they had the secret key, effectively compromising the card's security.

The consequences of this attack are concerning. Firstly, any data stored on the MIFARE Classic card (except for the keys) can no longer be considered confidential. This poses a direct privacy risk when personal information is stored on the card, potentially exposing individuals' data.

The integrity and authenticity of card-stored data can no longer be guaranteed. This is particularly worrisome in applications where the card holds a certain value, such as loyalty points or digital currency, as attackers could manipulate these values.

Knowledge of plaintext or keystream is necessary to initiate brute force attacks for secret key recovery. The authors are actively developing efficient methods to recover arbitrary sector keys of MIFARE Classic cards, further emphasizing the need for improved security measures and migration to more secure RFID technologies like MIFARE DESFire or MIFARE Plus.

# References 

- https://oomphmade.com/blog/what-is-a-mifare-card
- https://tagbase.ksec.co.uk/about/mifare-classic/#:~:text=The%20MIFARE%20Classic%20card%20has,engineering%20the%20MIFARE%20Classic%20chip.
- https://www.cosic.esat.kuleuven.be/rfidsec09/Papers/MifareClassicTroubles.ppt
- https://link.springer.com/content/pdf/10.1007/978-3-540-85893-5_20.pdf
